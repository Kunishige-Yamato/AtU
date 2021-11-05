using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;
public class progress : MonoBehaviour
{
    //json受け取る用クラス
    jsonReceive jsonRec = new jsonReceive();

    void Start()
    {
        //情報をDBから受け取る
        jsonRec.ReceiveData();

    }
}

