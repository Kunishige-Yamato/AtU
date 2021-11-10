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

    void Start()
    {
        //敵情報をDBから受け取る
        bossList=jsonRec.ReceiveData();

        //クラス作成
        enemyApp =gameObject.AddComponent<enemyAppearance>();

        //敵の生成(id指定)
        enemyApp.Appearance(bossList[0]);
    }
}