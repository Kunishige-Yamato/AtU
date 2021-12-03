using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;
using scoreInfo;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class jsonReceive
{
    //ゲーム用受け取り用配列
    Boss[] bossList;
    MobEnemy[] enemyList;
    //スコア用受け取り用配列
    PersonalScore scoreList;


    //ボス情報保管リスト
    enemyInfoList enemyInfoList;

    //DB接続
    public IEnumerator ConnectDB(string id)
    {
        //ゲームシーンのDB接続
        if (SceneManager.GetActiveScene().name == "game_1")
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
            SetEnemyList(bossList, enemyList);
        }
        else if (SceneManager.GetActiveScene().name == "score")
        {
            //UnityWebRequestを生成
            //ボスの情報取得
            string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?get_score=personal&id="+id;
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
                string getScoreData = request.downloadHandler.text;
                scoreList = ReceiveScoreData(getScoreData);
            }
        }
    }

    public void SetEnemyList(Boss[] bossList, MobEnemy[] enemyList)
    {
        progress pro = GameObject.Find("Progress").GetComponent<progress>();
        pro.bossList = bossList;
        pro.enemyList = enemyList;
    }

    public PersonalScore GetPersonalScore()
    {
        return scoreList;
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

    //DBからスコアの情報を受け取る
    public PersonalScore ReceiveScoreData(string jsonString)
    {
        //データを分割して配列へ
        Regex regex = new Regex("^.|.$");
        jsonString = regex.Replace(jsonString, "");

        //jsonからオブジェクトに格納
        PersonalScore p_score = JsonUtility.FromJson<PersonalScore>(jsonString);

        return p_score;
    }

    //DBにスコア保存
    public IEnumerator SaveScoreData(string id, int mode, int dif, int score)
    {
        //難易度を設定
        string game_mode="";
        if (mode == 0) 
        {
            switch(dif)
            {
                case 0:
                    game_mode = "s_easy";
                    break;
                case 1:
                    game_mode = "s_normal";
                    break;
                case 2:
                    game_mode = "s_hard";
                    break;
                case 3:
                    game_mode = "s_crazy";
                    break;
            }
        }
        else if(mode==1)
        {
            switch(PlayerPrefs.GetInt("gambling"))
            {
                case 0:
                    game_mode = "e_normal";
                    break;
                case 1:
                    game_mode = "e_gamble";
                    break;
            }
        }

        //UnityWebRequestを生成
        //ボスの情報取得
        Debug.Log("before set:" + id + "," + game_mode + "," + score);
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?id="+id+"&game_mode="+game_mode+"&put_score="+score;
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
            string result = request.downloadHandler.text;
            Debug.Log(result);
        }
    }
}
