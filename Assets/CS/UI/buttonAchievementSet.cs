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
    }

    void FixedUpdate()
    {

    }

    public void Onclick()
    {
        //賞品がスキンの時or称号の時
        if(acqSkin.activeSelf)
        {
            Image mySprite = acqSkin.GetComponent<Image>();
            //まだスキンがセットされてない時
            if (preSkin.sprite != mySprite.sprite)
            {
                //スキン変更
                preSkin.sprite = mySprite.sprite;
            }
        }
        else if(acqTitle.activeSelf)
        {
            Text myTitle = acqTitle.GetComponent<Text>();
            //まだ称号がセットされてない時
            if(preTitle.text!=myTitle.text)
            {
                //称号変更
                preTitle.text = myTitle.text;
            }
        }
    }
}
