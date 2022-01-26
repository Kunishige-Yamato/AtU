using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonAchievementSet : MonoBehaviour
{
    //プレビュー
    GameObject previewSkin;
    GameObject previewTitle;
    Image preSkin;
    Text preTitle;

    //ボタンにセットされてるやつ
    GameObject acqSkin;
    GameObject acqTitle;

    //報酬番号
    int achieveNum;

    //タイトル戻るボタンのコンポーネント
    //押されたタイミングでdecorationを保存
    buttonAchievementSave achieveSave,backBtn;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {
        if (gameObject.name != "SkinPreview" && gameObject.name != "TitlePreview") 
        { 
            //プレビュー
            previewSkin = GameObject.Find("SkinPreview/Image");
            preSkin = previewSkin.GetComponent<Image>();
            previewTitle = GameObject.Find("TitlePreview");
            preTitle = previewTitle.GetComponent<Text>();

            //自分のセットされてるやつ取得
            acqSkin = gameObject.transform.Find("AcquiredSkin").gameObject;
            acqTitle = gameObject.transform.Find("AcquiredTitle").gameObject;
        }

        achieveSave = GameObject.Find("Canvas/DecoEnterButton").GetComponent<buttonAchievementSave>();
        backBtn = GameObject.Find("Canvas/BackTitleButton").GetComponent<buttonAchievementSave>();

        //最初は1をセット
        if (achieveSave.GetDefaultDeco()[0] == 0) 
        {
            achieveSave.SetSkinNum(1);
            backBtn.SetSkinNum(1);
        }
        if (achieveSave.GetDefaultDeco()[1] == 0) 
        {
            achieveSave.SetTitleNum(1);
            backBtn.SetTitleNum(1);
        }
    }

    void FixedUpdate()
    {

    }

    public void SetAchieveNum(int num)
    {
        achieveNum = num;
    }

    public void Onclick()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        if (gameObject.name == "SkinPreview")
        {
            Image mySprite = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            //スキン変更
            mySprite.sprite = Resources.Load<Sprite>("AchievementImage/default"); ;
            achieveSave.SetSkinNum(1);
            backBtn.SetSkinNum(1);
        }
        else if (gameObject.name == "TitlePreview")
        {
            Text myTitle = gameObject.GetComponent<Text>();
            //称号変更
            myTitle.text = "";
            achieveSave.SetTitleNum(1);
            backBtn.SetTitleNum(1);
        }
        else
        {
            //賞品がスキンの時or称号の時
            if (acqSkin.activeSelf)
            {
                Image mySprite = acqSkin.GetComponent<Image>();
                //スキン変更
                if (preSkin.sprite != mySprite.sprite)
                {
                    //スキン変更
                    preSkin.sprite = mySprite.sprite;
                    achieveSave.SetSkinNum(achieveNum);
                    backBtn.SetSkinNum(achieveNum);
                }
            }
            else if (acqTitle.activeSelf)
            {
                Text myTitle = acqTitle.GetComponent<Text>();
                //称号変更
                if (preTitle.text != myTitle.text)
                {
                    //称号変更
                    preTitle.text = myTitle.text;
                    achieveSave.SetTitleNum(achieveNum);
                    backBtn.SetTitleNum(achieveNum);
                }
            }
        }
    }
}
