using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class achievementManager : MonoBehaviour
{
    //プレビュー
    public Image settingSkin;
    GameObject settingTitle;
    GameObject settingName;

    //トロフィーPrefab
    public GameObject trophyPrefab;

    //スクロール部品のcontent
    public GameObject parentContent;

    //テスト用獲得スキン
    public Sprite testSkin;


    void Start()
    {
        //プレビュー用オブジェ取得
        settingTitle = GameObject.Find("TitlePreview");
        settingName = GameObject.Find("NamePreview");

        //プレビューのテキスト
        Text settingTitleText = settingTitle.GetComponent<Text>();
        Text settingNameText = settingName.GetComponent<Text>();

        //プレビュー初期セット
        settingTitleText.text = "私は大砲よ";
        settingNameText.text = "ユーザー01";

        //トロフィー作成
        for(int i=0;i<20;i++)
        {
            GameObject trophy = Instantiate(trophyPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            trophy.transform.SetParent(parentContent.transform);

            //ここでデータを書き換える

            //Achievement Hint
            GameObject troHint = trophy.transform.Find("AchievementHint").gameObject;
            Text hintText = troHint.GetComponent<Text>();
            hintText.text = "最初の一歩";

            //Achievement Explanation
            GameObject troExpl = trophy.transform.Find("AchievementExplanation").gameObject;
            Text explText = troExpl.GetComponent<Text>();
            explText.text = "累計" + (i*100) + "回クリアする。";

            //獲得商品。スキンか称号かのどちらか
            GameObject troSkin = trophy.transform.Find("Button/AcquiredSkin").gameObject;
            GameObject troTitle = trophy.transform.Find("Button/AcquiredTitle").gameObject;
            //テスト条件
            if (i%2!=0)
            {
                //スキンの場合
                Image acqSkin = troSkin.GetComponent<Image>();
                //テスト用画像を設定，挿入
                acqSkin.sprite = testSkin;
                troTitle.SetActive(false);
            }
            else
            {
                //称号の場合
                Text acqText = troTitle.GetComponent<Text>();
                acqText.text = "バキバキ童貞" + i/2;
                troSkin.SetActive(false);
            }

            //トロフィー獲得条件を満たしていたら
            //テスト用条件
            if(i<10)
            {
                //グレーを解除，？？？を解除
                GameObject interference = trophy.transform.Find("NotReleasedPanel").gameObject;
                Destroy(interference);
                interference = trophy.transform.Find("Button/q").gameObject;
                Destroy(interference);
            }
            else
            {
                //獲得商品を非表示に
                Destroy(troSkin);
                Destroy(troTitle);

                //全ての文字を対象にする正規表現
                Regex reg = new Regex(".");
                explText.text=reg.Replace(explText.text, "?");
            }
        }
    }

    void FixedUpdate()
    {
        
    }
}
