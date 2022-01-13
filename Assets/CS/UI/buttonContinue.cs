using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonContinue : MonoBehaviour
{
    public CanvasGroup pauseGroup;

    GameObject player;
    player pl;

    //SE関係
    public AudioClip[] audioSEClips;

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
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

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
