using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;
using System.Text.RegularExpressions;

public class jsonReceive
{
    //ボス情報保管リスト
    enemyInfoList enemyInfoList;

    //受け取り用string
    string jsonData;

    //DBから情報を受け取る
    public Boss[] ReceiveBossData()
    {
        //リストを作る
        enemyInfoList = new enemyInfoList(4,"boss");

        //jsonを受け取ってjsondataにぶちこむ
        //仮データ
        jsonData = "[{\"name\":\"boss1\",\"hp\":10,\"hitBonus\":20,\"defeatBonus\":5000,\"timeBonus\":3000},{\"name\":\"boss2\",\"hp\":800,\"hitBonus\":20,\"defeatBonus\":8000,\"timeBonus\":5000}]";

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

            enemyInfoList.SetBossInfo(boss.name, boss.hp, boss.hitBonus, boss.defeatBonus, boss.timeBonus);
        }

        return enemyInfoList.boss;
    }

    //DBから情報を受け取る
    public MobEnemy[] ReceiveEnemyData()
    {
        //リストを作る
        enemyInfoList = new enemyInfoList(10, "enemy");

        //jsonを受け取ってjsondataにぶちこむ
        //仮データ
        jsonData = "[{\"name\":\"enemy1\",\"hp\":5,\"hitBonus\":20,\"defeatBonus\":100,\"fallSpeed\":0,\"moveSpeed\":0,\"rotSpeed\":0,\"lifeExpectancy\":10},{\"name\":\"enemy2\",\"hp\":5,\"hitBonus\":20,\"defeatBonus\":100,\"fallSpeed\":0,\"moveSpeed\":-0.2,\"rotSpeed\":0,\"lifeExpectancy\":5},{\"name\":\"enemy3\",\"hp\":5,\"hitBonus\":20,\"defeatBonus\":100,\"fallSpeed\":0,\"moveSpeed\":0,\"rotSpeed\":0,\"lifeExpectancy\":15}]";

        //必要部分だけ抜き取る
        //配列の[]とデータ間の,を置き換え
        Regex regex = new Regex("^.|.$");
        jsonData = regex.Replace(jsonData, "");
        regex = new Regex("},{");
        jsonData = regex.Replace(jsonData, "}@{");

        //データを分割して配列へ
        var jsonDatas = jsonData.Split('@');

        //jsonからオブジェクトに格納
        for (int i = 0; i < jsonDatas.Length; i++)
        {
            MobEnemy enemy = JsonUtility.FromJson<MobEnemy>(jsonDatas[i]);

            enemyInfoList.SetEnemyInfo(enemy.name, enemy.hp, enemy.hitBonus, enemy.defeatBonus, enemy.fallSpeed, enemy.moveSpeed, enemy.rotSpeed, enemy.lifeExpectancy);
        }

        return enemyInfoList.enemy;
    }
}
