using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;
public class progress : MonoBehaviour
{
    //json受け取る用クラス
    jsonReceive jsonRec = new jsonReceive();

    //ボスの情報リスト
    Boss[] bossList;

    //敵生成クラス
    enemyAppearance enemyApp;

    //難易度
    int difficulty;

    //UI
    public CanvasGroup resultGroup;
    public CanvasGroup pauseGroup;
    public CanvasGroup hpBarGroup;

    void Start()
    {
        //敵情報をDBから受け取る
        bossList=jsonRec.ReceiveData();

        //クラス作成
        enemyApp =gameObject.AddComponent<enemyAppearance>();

        //敵の生成(name指定)
        enemyApp.Appearance(bossList[0]);

        //hpバー消去
        //hpBarGroup.alpha = 0f;

        //リザルト消去
        resultGroup.alpha = 0f;
        resultGroup.interactable = false;

        //ポーズ画面消去
        pauseGroup.alpha = 0f;
        pauseGroup.interactable = false;

        //難易度設定
        //仮
        difficulty = 2;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }
}

/*
次回予告
boss1攻撃残り
残りのボスの攻撃
progressの進行に沿ったあれこれ
 */