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

    void Start()
    {
        //プレビュー
        previewSkin = GameObject.Find("SkinPreview/Image");
        preSkin = previewSkin.GetComponent<Image>();
        previewTitle = GameObject.Find("TitlePreview");
        preTitle = previewTitle.GetComponent<Text>();

        //自分のセットされてるやつ取得
        acqSkin = gameObject.transform.Find("AcquiredSkin").gameObject;
        acqTitle = gameObject.transform.Find("AcquiredTitle").gameObject;

        achieveSave = GameObject.Find("Canvas/DecoEnterButton").GetComponent<buttonAchievementSave>();
        backBtn = GameObject.Find("Canvas/BackTitleButton").GetComponent<buttonAchievementSave>();
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
        //賞品がスキンの時or称号の時
        if(acqSkin.activeSelf)
        {
            Image mySprite = acqSkin.GetComponent<Image>();
            //スキン変更
            if (preSkin.sprite != mySprite.sprite)
            {
                //スキン変更
                preSkin.sprite = mySprite.sprite;
                achieveSave.GetSkinNum(achieveNum);
                backBtn.GetSkinNum(achieveNum);
            }
        }
        else if(acqTitle.activeSelf)
        {
            Text myTitle = acqTitle.GetComponent<Text>();
            //称号変更
            if(preTitle.text!=myTitle.text)
            {
                //称号変更
                preTitle.text = myTitle.text;
                achieveSave.GetTitleNum(achieveNum);
                backBtn.GetTitleNum(achieveNum);
            }
        }
    }
}
