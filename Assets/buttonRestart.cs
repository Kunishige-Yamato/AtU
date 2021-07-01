using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonRestart : MonoBehaviour
{
    public GameObject EG;
    EnemyGenerator eg;
    public CanvasGroup pauseGroup;

    void Start()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void OnClick()
    {
        eg=EG.GetComponent<EnemyGenerator>();

        Clear();

        //ポーズ画面非表示
        pauseGroup.alpha=0f;
        pauseGroup.interactable=false;

        //各変数のリセット
        eg.timer=0;
        eg.sumTime=0;
        eg.stageNum=0;

        eg.ReadFile();
    }

    void Clear()
    {
        //タグつきを全て格納
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys) {
            Destroy(del);
        }
        GameObject[] bullets=GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject del in enemys) {
            Destroy(del);
        }
    }
}
