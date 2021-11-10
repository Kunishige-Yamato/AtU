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
    public Boss[] ReceiveData()
    {
        //リストを作る
        bossInfoList = new enemyInfoList(4);

        //jsonを受け取ってjsondataにぶちこむ
        //仮データ
        jsonData = "[{\"name\":\"boss1\",\"posX\":0,\"posY\":3,\"hp\":10,\"hitBonus\":20,\"defeatBonus\":5000,\"timeBonus\":3000},{\"name\":\"boss2\",\"posX\":0,\"posY\":3,\"hp\":800,\"hitBonus\":20,\"defeatBonus\":8000,\"timeBonus\":5000}]";

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

            bossInfoList.SetBossInfo(boss.name, boss.posX, boss.posY, boss.hp, boss.hitBonus, boss.defeatBonus, boss.timeBonus);
        }

        return bossInfoList.boss;
    }
}
