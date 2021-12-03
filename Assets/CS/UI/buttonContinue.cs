using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonContinue : MonoBehaviour
{
    public CanvasGroup pauseGroup;

    GameObject player;
    player pl;

    void Start()
    {
        player=GameObject.Find("Player");
        pl=player.GetComponent<player>();
    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        //ポーズ画面消去
        pauseGroup.alpha=0f;
        pauseGroup.interactable = false;
        pauseGroup.blocksRaycasts = false;

        pl.canMove=true;

        if (Cursor.visible==true)
        {
            //カーソル消去
            Cursor.lockState=CursorLockMode.Confined; //はみ出さないモード
            Cursor.visible=false; //OSカーソル非表示
        }
    }
}
