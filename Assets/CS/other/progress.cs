using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enemyInfo;

public class progress : MonoBehaviour
{
    //json受け取る用クラス
    jsonReceive jsonRec = new jsonReceive();

    //ボスの情報リスト
    public Boss[] bossList;
    //エネミーの情報リスト
    public MobEnemy[] enemyList;

    //敵生成クラス
    enemyAppearance enemyApp;

    //難易度，モード
    int difficulty,endless;

    //自機オブジェクト
    GameObject player;
    player pl;

    //ステージ番号管理
    public int stageNum = 0;
    public int allStageNum = 4;

    //CSVファイル
    TextAsset csvFile;
    // CSVの中身を入れるリスト;
    List<string[]> csvDatas = new List<string[]>();

    //タイマー
    float timer;

    //UI
    public CanvasGroup resultGroup, pauseGroup, hpBarGroup;
    public Text stageText, scoreText, timeText, hitText;
    int stageScore = 0, stageHit = 0;
    //背景
    public GameObject backGround;
    public Sprite[] backGroundSprites;

    //カットイン関係
    public GameObject cutInCanvas;
    Animator animator;
    public Sprite[] cutImage;

    void Start()
    {
        //敵情報をDBから受け取る
        StartCoroutine(jsonRec.ConnectDB(PlayerPrefs.GetString("ID")));

        //クラス作成
        enemyApp =gameObject.AddComponent<enemyAppearance>();

        //敵の生成(name指定)
        //enemyApp.BossAppearance(bossList[0]);

        //hpバー消去
        hpBarGroup.alpha = 0f;

        //リザルト消去
        resultGroup.alpha = 0f;
        resultGroup.interactable = false;

        //ポーズ画面消去
        pauseGroup.alpha = 0f;
        pauseGroup.interactable = false;

        //自機取得
        player = GameObject.Find("Player");
        pl = player.GetComponent<player>();

        //難易度設定
        //仮
        difficulty = 0;
        endless = 0;

        //Debug用ステージスキップ
        stageNum = 1;

        //カットイン設定
        animator = cutInCanvas.GetComponent<Animator>();

        StageStart();
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        for(int i=0;i<csvDatas.Count;i++)
        {
            if(float.Parse(csvDatas[i][2])<=timer&&csvDatas[i][5]=="0")
            {
                if(csvDatas[i][0]=="boss"&&bossList.Length>0)
                {
                    int bossNum = int.Parse(csvDatas[i][1]);
                    float posX = float.Parse(csvDatas[i][3]);
                    float posY = float.Parse(csvDatas[i][4]);
                    //Debug.Log(bossNum);
                    enemyApp.BossAppearance(bossList[bossNum - 1], posX, posY);
                    hpBarGroup.alpha = 1f;
                }
                else if(csvDatas[i][0]=="enemy"&&enemyList.Length>0)
                {
                    int enemyNum = int.Parse(csvDatas[i][1]);
                    float posX = float.Parse(csvDatas[i][3]);
                    float posY = float.Parse(csvDatas[i][4]);
                    //Debug.Log(enemyNum);
                    enemyApp.EnemyAppearance(enemyList[enemyNum - 1], posX, posY);
                }
                else if(csvDatas[i][0]=="bullet")
                {
                    string bulName = "bullet" + csvDatas[i][1] + "Prefab";
                    float posX = float.Parse(csvDatas[i][3]);
                    float posY = float.Parse(csvDatas[i][4]);
                    //Debug.Log(bulName);
                    enemyApp.BulletAppearance(bulName, posX, posY);
                }
                else
                {
                    Debug.Log(csvDatas[i][0]+"はcsvの記述が不適切");
                }
                csvDatas[i][5]="1";
            }
        }
    }

    public int[] GetDifficulty()
    {
        int[] dif = { difficulty, endless };
        return dif;
    }

    public int[] GetStageNum()
    {
        int[] stage = { stageNum, allStageNum };
        return stage;
    }

    public void DisplayResult()
    {
        //自機停止
        pl.canMove = false;

        //カーソル表示
        Cursor.visible = true;

        //hpバー消去
        hpBarGroup.alpha = 0f;

        //敵消去
        //タグつきを全て格納
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys)
        {
            Destroy(del);
        }

        //リザルト表示
        resultGroup.alpha = 1f;
        resultGroup.interactable = true;

        //スコア集計＆表示
        stageText.text = "Stage-" + stageNum;
        GameObject scoreCounter = GameObject.Find("ScoreCounter");
        ScoreCount sc = scoreCounter.GetComponent<ScoreCount>();
        scoreText.text = "Score:" + (sc.GetScore() - stageScore);
        stageScore = sc.GetScore();
        timeText.text = "Time:" + (Mathf.Floor(sc.GetTime()[0] * 100) / 100);
        hitText.text = "Hit:" + (pl.hitNum - stageHit);
        stageHit = pl.hitNum;
    }

    public void StageStart()
    {
        //自機動作
        Cursor.visible = false;
        pl.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("CursorMove", 1.8f);

        //hpバー消去
        hpBarGroup.alpha = 0f;

        //リザルト消去
        resultGroup.alpha = 0f;
        resultGroup.interactable = false;

        //ポーズ画面消去
        pauseGroup.alpha = 0f;
        pauseGroup.interactable = false;

        //背景設定
        backGround.GetComponent<Image>().sprite = backGroundSprites[stageNum];

        stageNum++;

        CutIn();

        if (stageNum <= allStageNum)
        {
            //csv読み込み
            switch (difficulty)
            {
                case 0:
                    csvFile = Resources.Load("CSV/stage-e-" + stageNum) as TextAsset;
                    break;
                case 1:
                    csvFile = Resources.Load("CSV/stage-n-" + stageNum) as TextAsset;
                    break;
                case 2:
                    csvFile = Resources.Load("CSV/stage-h-" + stageNum) as TextAsset;
                    break;
                case 3:
                    csvFile = Resources.Load("CSV/stage-c-" + stageNum) as TextAsset;
                    break;
            }
            StringReader reader = new StringReader(csvFile.text);

            //テスト用ボス出現
            //string boss="boss"+stageNum;
            //Generate(boss,0,3);


            // , で分割しつつ一行ずつ読み込み，リストに追加していく
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                csvDatas.Add(line.Split(','));
            }

            timer = 0;
            //Debug用ステージ早送り
            timer = 60;
        }
    }

    void CursorMove()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void CutIn()
    {
        switch (stageNum)
        {
            case 1:
                cutInCanvas.GetComponent<Image>().sprite = cutImage[0];
                break;
            case 2:
                cutInCanvas.GetComponent<Image>().sprite = cutImage[1];
                break;
            case 3:
                cutInCanvas.GetComponent<Image>().sprite = cutImage[2];
                break;
            case 4:
                cutInCanvas.GetComponent<Image>().sprite = cutImage[3];
                break;
        }
        animator.Play("CutIn", 0, 0f);
    }
}