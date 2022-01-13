using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enemyInfo;
using scoreInfo;
using achievementInfo;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class jsonReceive
{
    //ゲーム用受け取り用配列
    Boss[] bossList;
    MobEnemy[] enemyList;
    //スコア用受け取り用配列
    PersonalScore p_score;
    RankingScore[] rankingScore;
    //称号用受け取り配列
    AchievementList[] achieve;
    string[] myAchieve;
    string[] mySetAchieve;


    //ボス情報保管リスト
    enemyInfoList enemyInfoList;

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
        SetEnemyList(bossList, enemyList);
    }

    public void SetEnemyList(Boss[] bossList, MobEnemy[] enemyList)
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

    //DBから自分のスコアの情報を受け取る
    public IEnumerator ReceivePersonalScoreData(string id)
    {
        //自分のスコアの情報取得
        string getScoreData="";

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?get_score=personal&id=" + id;
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
            getScoreData = request.downloadHandler.text;
        }

        //データを分割して配列へ
        Regex regex = new Regex("^.|.$");
        getScoreData = regex.Replace(getScoreData, "");

        //jsonからオブジェクトに格納
        p_score = JsonUtility.FromJson<PersonalScore>(getScoreData);

        yield return null;
    }

    public PersonalScore GetPersonalScore()
    {
        return p_score;
    }

    //DBからワールドランキングの情報を受け取る
    public IEnumerator ReceiveRankingScoreData(string id, string kind)
    {
        //全体ランキングのスコアの情報取得
        string jsonString = "";

        //kindは "world" or "friend"
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?get_score=" + kind + "&id=" + id;
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
            jsonString = request.downloadHandler.text;
        }

        //データを分割して配列へ
        Regex regex = new Regex("^.|.$");
        jsonString = regex.Replace(jsonString, "");
        regex = new Regex("},{");
        jsonString = regex.Replace(jsonString, "}&{");

        //Debug用
        /*if (kind == "world")
        {
            jsonString = "{\"achieve\":\"HelloWorld\",\"name\":\"testUser_1\",\"score\":33333}&{\"achieve\":\"永遠の二番手\",\"name\":\"testUser_2\",\"score\":22222}&{\"achieve\":\"末っ子\",\"name\":\"testUser_3\",\"score\":11111}";
        }
        else
        {
            jsonString = "{\"achieve\":\"友達一号\",\"name\":\"testUser_4\",\"score\":666666}&{\"achieve\":\"友達候補\",\"name\":\"testUser_5\",\"score\":55555}&{\"achieve\":\"N村くん\",\"name\":\"testUser_6\",\"score\":44444}";
        }*/
        var jsonDatas = jsonString.Split('&');

        //jsonからオブジェクトに格納
        RankingScore[] r_score=new RankingScore[jsonDatas.Length];
        for (int i = 0; i < jsonDatas.Length; i++)
        {
            r_score[i] = JsonUtility.FromJson<RankingScore>(jsonDatas[i]);
        }
        rankingScore = r_score;

        yield return null;
    }

    public RankingScore[] GetRankingScore()
    {
        return rankingScore;
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
        }
    }

    public IEnumerator RecieveAchievement()
    {
        //称号の情報取得
        string jsonString = "";

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?get_achievement=all";
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
            jsonString = request.downloadHandler.text;
        }

        //データを分割して配列へ
        Regex regex = new Regex("^.|.$");
        jsonString = regex.Replace(jsonString, "");
        regex = new Regex("},{");
        jsonString = regex.Replace(jsonString, "}&{");

        //jsonString = "{\"id\":1,\"type\":\"title\",\"achieveID\":\"1\",\"hintText\":\"はじめの一歩\",\"explanationText\":\"ゲームを一回プレイする。\",\"achievement\":\"HelloWorld\"}&{\"id\":2,\"type\":\"title\",\"achieveID\":\"2\",\"hintText\":\"がんばろーる\",\"explanationText\":\"猛者はスコープを使わないって？\",\"achievement\":\"私は大砲よ\"}&{\"id\":3,\"type\":\"skin\",\"achieveID\":\"1\",\"hintText\":\"クリップは髪留めのこと\",\"explanationText\":\"これはマガジンよ(ドヤ)\",\"achievement\":\"banga.png\"}&{\"id\":4,\"type\":\"title\",\"achieveID\":\"3\",\"hintText\":\"顔が性犯罪者\",\"explanationText\":\"PHPでショッピングサイトを作成する。\",\"achievement\":\"Mr.KUNII\"}&{\"id\":5,\"type\":\"skin\",\"achieveID\":\"2\",\"hintText\":\"開発者\",\"explanationText\":\"テストの時はこのスキンだった\",\"achievement\":\"SelfMachine.png\"}";
        var jsonDatas = jsonString.Split('&');

        //jsonからオブジェクトに格納
        AchievementList[] a_list = new AchievementList[jsonDatas.Length];
        for (int i = 0; i < jsonDatas.Length; i++)
        {
            a_list[i] = JsonUtility.FromJson<AchievementList>(jsonDatas[i]);
        }
        achieve = a_list;

        yield return null;
    }

    public AchievementList[] GetAchievementList()
    {
        return achieve;
    }

    public IEnumerator RecieveMyAchieve(string id)
    {
        //自分のスコアの情報取得
        string getAchieveData = "";

        //自分の獲得済みのトロフィーを取得
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?get_achievement=mine&id=" + id;
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
            getAchieveData = request.downloadHandler.text;
        }

        //データを分割して配列へ
        //dubug用
        //getAchieveData = "1,3,4,5";

        //カンマ区切りで配列格納
        myAchieve = getAchieveData.Split(',');

        //自分の今セットしてるやつ取得
        url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?get_achievement=set&id=" + id;
        request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // phpから受け取った値を&で区切って配列を生成
            getAchieveData = request.downloadHandler.text;
        }

        //データを分割して配列へ
        //dubug用
        //getAchieveData = "1,1";

        //カンマ区切りで配列格納
        //[title],[skin]の順でIDが来る
        mySetAchieve = getAchieveData.Split(',');
    }

    public string[] GetMyAchieve()
    {
        return myAchieve;
    }

    public string[] GetMySetAchieve()
    {
        return mySetAchieve;
    }

    public IEnumerator SaveDecoration(string id, int titleNum, int skinNum)
    {
        //プレビューにある
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?set_title=" + titleNum + "&set_skin=" + skinNum + "&id=" + id;
        UnityWebRequest request = UnityWebRequest.Get(url);

        //SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            //カンマ区切りで配列格納
            Debug.Log("saveDeco:" + request.downloadHandler.text);
            string[] decoration = request.downloadHandler.text.Split(',');
            Debug.Log(decoration[0] + "," + decoration[1]);

            PlayerPrefs.SetString("TITLE", decoration[0]);
            PlayerPrefs.SetString("SKIN", decoration[1]);
            PlayerPrefs.Save();
        }
    }

    public IEnumerator SaveUserData(string id, int hitNum, int stageNum)
    {
        //UserData保存
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?id="+id+"&hit_num="+hitNum+"&stage_num="+stageNum;
        UnityWebRequest request = UnityWebRequest.Get(url);

        //SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    public IEnumerator SaveHiddenCommand(string id, int kind)
    {
        //UserData保存
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?id=" + id + "&command=" + kind;
        UnityWebRequest request = UnityWebRequest.Get(url);

        //SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            PlayerPrefs.SetString("COMMAND", request.downloadHandler.text);
            PlayerPrefs.Save();
            Debug.Log(request.downloadHandler.text);
        }
    }
}