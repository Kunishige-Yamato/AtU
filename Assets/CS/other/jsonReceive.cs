using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class jsonReceive
{
    //ボス情報保管リスト
    enemyInfoList enemyInfoList;

    //受け取り用配列
    Boss[] bossList;
    MobEnemy[] enemyList;

    //DB接続
    public IEnumerator ConnectDB(string id)
    {
        //UnityWebRequestを生成
        //ボスの情報取得
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?enemy=Boss";
        UnityWebRequest request = UnityWebRequest.Get(url);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // phpから受け取った値を&で区切って配列を生成
            string getBossData = request.downloadHandler.text;
            bossList = ReceiveBossData(getBossData);
        }
        //モブの情報取得
        url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?enemy=Mob";
        request = UnityWebRequest.Get(url);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // phpから受け取った値を&で区切って配列を生成
            string getEnemyData = request.downloadHandler.text;
            enemyList = ReceiveEnemyData(getEnemyData);
        }
        SetEnemyList();
    }

    public void SetEnemyList()
    {
        progress pro = GameObject.Find("Progress").GetComponent<progress>();
        pro.bossList = bossList;
        pro.enemyList = enemyList;
    }

    //DBからbossの情報を受け取る
    public Boss[] ReceiveBossData(string jsonString)
    {
        //jsonを受け取ってjsondataにぶちこむ
        //仮データ
        //jsonString = "[{\"name\":\"boss1\",\"hp\":10,\"hitBonus\":20,\"defeatBonus\":5000,\"timeBonus\":3000}&{\"name\":\"boss2\",\"hp\":800,\"hitBonus\":20,\"defeatBonus\":8000,\"timeBonus\":5000}]";

        //データを分割して配列へ
        Regex regex = new Regex("^.|.$");
        jsonString = regex.Replace(jsonString, "");
        regex = new Regex("},{");
        jsonString = regex.Replace(jsonString, "}&{");

        var jsonDatas = jsonString.Split('&');

        //リストを作る
        enemyInfoList = new enemyInfoList(jsonDatas.Length, "boss");

        //jsonからオブジェクトに格納
        for (int i=0;i<jsonDatas.Length;i++)
        {
            Boss boss = JsonUtility.FromJson<Boss>(jsonDatas[i]);

            enemyInfoList.SetBossInfo(boss.name, boss.hp, boss.hitBonus, boss.defeatBonus, boss.timeBonus);
        }

        return enemyInfoList.boss;
    }

    //DBからmobEnemyの情報を受け取る
    public MobEnemy[] ReceiveEnemyData(string jsonString)
    {
        //jsonを受け取ってjsondataにぶちこむ
        //仮データ
        //jsonString = "[{\"name\":\"enemy1\",\"hp\":5,\"hitBonus\":20,\"defeatBonus\":100,\"fallSpeed\":0,\"moveSpeed\":0,\"rotSpeed\":0,\"lifeExpectancy\":10}&{\"name\":\"enemy2\",\"hp\":5,\"hitBonus\":20,\"defeatBonus\":100,\"fallSpeed\":0,\"moveSpeed\":-0.2,\"rotSpeed\":0,\"lifeExpectancy\":5}&{\"name\":\"enemy3\",\"hp\":5,\"hitBonus\":20,\"defeatBonus\":100,\"fallSpeed\":0,\"moveSpeed\":0,\"rotSpeed\":0,\"lifeExpectancy\":15}]";

        //データを分割して配列へ
        Regex regex = new Regex("^.|.$");
        jsonString = regex.Replace(jsonString, "");
        regex = new Regex("},{");
        jsonString = regex.Replace(jsonString, "}&{");

        var jsonDatas = jsonString.Split('&');

        //リストを作る
        enemyInfoList = new enemyInfoList(jsonDatas.Length, "enemy");

        //jsonからオブジェクトに格納
        for (int i = 0; i < jsonDatas.Length; i++)
        {
            MobEnemy enemy = JsonUtility.FromJson<MobEnemy>(jsonDatas[i]);

            enemyInfoList.SetEnemyInfo(enemy.name, enemy.hp, enemy.hitBonus, enemy.defeatBonus, enemy.fallSpeed, enemy.moveSpeed, enemy.rotSpeed, enemy.lifeExpectancy);
        }

        return enemyInfoList.enemy;
    }
}
