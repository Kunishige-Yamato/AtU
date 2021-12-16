using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using achievementInfo;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace achievementInfo
{
    public class AchievementList
    {
        public int id;
        public string type;
        public int achieveID;
        public string hintText;
        public string exp;
        public string s_or_t;

        public AchievementList(int id, string type, int achieveID, string hintText, string exp, string s_or_t)
        {
            this.id = id;
            this.type = type;
            this.achieveID = achieveID;
            this.hintText = hintText;
            this.exp = exp;
            this.s_or_t = s_or_t;
        }
    }
}

public class achievementManager : MonoBehaviour
{
    //プレビュー
    Image settingSkin;
    GameObject settingTitle;
    GameObject settingName;

    //報酬リスト
    jsonReceive jsonRec = new jsonReceive();
    AchievementList[] achievementList;

    //トロフィーPrefab
    public GameObject trophyPrefab;

    //スクロール部品のcontent
    public GameObject parentContent;

    void Start()
    {
        //プレビュー用オブジェ取得
        settingSkin = GameObject.Find("SkinPreview/Image").GetComponent<Image>();
        settingTitle = GameObject.Find("TitlePreview");
        settingName = GameObject.Find("NamePreview");

        //プレビュー名前セット
        settingName.GetComponent<Text>().text = PlayerPrefs.GetString("NAME");

        //報酬リスト受け取り，作成
        StartCoroutine(GetAchieveList());
    }

    public IEnumerator GetAchieveList()
    {
        yield return StartCoroutine(jsonRec.RecieveAchievement());
        achievementList = jsonRec.GetAchievementList();

        //解放済みの称号を確認
        yield return StartCoroutine(jsonRec.RecieveMyAchieve(PlayerPrefs.GetString("ID")));
        string[] myAchieve = jsonRec.GetMyAchieve();
        string[] mySetAchieve = jsonRec.GetMySetAchieve();

        //トロフィー作成
        for (int i = 0; i < achievementList.Length; i++)
        {
            GameObject trophy = Instantiate(trophyPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            trophy.transform.SetParent(parentContent.transform);

            //ここでデータを書き換える

            //Achievement Hint
            GameObject troHint = trophy.transform.Find("AchievementHint").gameObject;
            Text hintText = troHint.GetComponent<Text>();
            hintText.text = achievementList[i].hintText;

            //Achievement Explanation
            GameObject troExpl = trophy.transform.Find("AchievementExplanation").gameObject;
            Text explText = troExpl.GetComponent<Text>();
            explText.text = achievementList[i].exp;

            //獲得商品。スキンか称号かのどちらか
            GameObject troBtn = trophy.transform.Find("Button").gameObject;
            GameObject troSkin = troBtn.transform.Find("AcquiredSkin").gameObject;
            GameObject troTitle = troBtn.transform.Find("AcquiredTitle").gameObject;
            //報酬番号設定
            troBtn.GetComponent<buttonAchievementSet>().SetAchieveNum(achievementList[i].achieveID);
            //スキンと称号判定
            if (achievementList[i].type == "skin") 
            {
                //スキンの場合
                Image acqSkin = troSkin.GetComponent<Image>();
                //テスト用画像を設定，挿入
                Sprite skinSprite = Resources.Load<Sprite>("AchievementImage/" + Regex.Replace(achievementList[i].s_or_t, @"\.png$", ""));
                acqSkin.sprite = skinSprite;

                troTitle.SetActive(false);

                //セットされているスキンだったらプレビューに反映
                if(achievementList[i].achieveID==int.Parse(mySetAchieve[1]))
                {
                    settingSkin.sprite = skinSprite;

                    //セット済みのスキン番号を渡す
                    GameObject.Find("Canvas/DecoEnterButton").GetComponent<buttonAchievementSave>().GetDefSkinNum(achievementList[i].achieveID);
                }
            }
            else if (achievementList[i].type == "title") 
            {
                //称号の場合
                Text acqText = troTitle.GetComponent<Text>();
                acqText.text = achievementList[i].s_or_t;

                troSkin.SetActive(false);

                //セットされている称号だったらプレビューに反映
                if (achievementList[i].achieveID == int.Parse(mySetAchieve[0]))
                {
                    Text settingTitleText = settingTitle.GetComponent<Text>();
                    settingTitleText.text = achievementList[i].s_or_t;

                    //セット済みの称号番号を渡す
                    GameObject.Find("Canvas/DecoEnterButton").GetComponent<buttonAchievementSave>().GetDefTitleNum(achievementList[i].achieveID);
                }
            }
            else
            {
                Debug.Log("typeが不正です。");
            }

            //解放されているか番号で確認
            for (int j = 0; j < myAchieve.Length; j++)
            {
                if (i == int.Parse(myAchieve[j])-1)
                {
                    //グレーを解除，？？？を解除
                    GameObject interference = trophy.transform.Find("NotReleasedPanel").gameObject;
                    Destroy(interference);
                    interference = trophy.transform.Find("Button/q").gameObject;
                    Destroy(interference);
                    break;
                }
                else if (j==myAchieve.Length-1)
                {
                    //獲得商品を非表示に
                    Destroy(troSkin);
                    Destroy(troTitle);

                    //説明文を？に置換
                    Regex reg = new Regex(".");
                    explText.text = reg.Replace(explText.text, "?");
                }
            }
        }
    }
}
