using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;
using System.Text.RegularExpressions;

public class jsonReceive
{
    //ボス情報保管リスト
    enemyInfoList bossInfoList;

    //受け取り用string
    string jsonData;

    //DBから情報を受け取る
    public void ReceiveData()
    {
        //リストを作る
        bossInfoList = new enemyInfoList(4);


        //jsonを受け取ってjsondataにぶちこむ
        //仮データ
        jsonData = "[{\"name\":\"boss1\",\"hp\":10000,\"hitBonus\":20,\"defeatBonus\":5000,\"timeBonus\":3000},{\"name\":\"boss2\",\"hp\":20000,\"hitBonus\":20,\"defeatBonus\":8000,\"timeBonus\":5000}]";
        Debug.Log(jsonData);

        //必要部分だけ抜き取る
        //配列の[]とデータ間の,を置き換え
        Regex regex = new Regex("^.|.$");
        jsonData = regex.Replace(jsonData, "");
        regex = new Regex("},{");
        jsonData = regex.Replace(jsonData, "}@{");

        //データを分割して配列へ
        var jsonDatas = jsonData.Split('@');

        //jsonからオブジェクトに格納
        for(int i=0;i<jsonDatas.Length;i++)
        {

            Boss boss = JsonUtility.FromJson<Boss>(jsonDatas[i]);

            bossInfoList.SetBossInfo("\""+boss.name+"\"", boss.hp, boss.hitBonus, boss.defeatBonus, boss.timeBonus);
            boss.PrintBossInfo();
        }
    }
}
