using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enemyInfo;
using System.Linq;

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

    //ボスランダム
    int bossRan;
    //ボス選択配列初期化
    int[] selectBoss = new int[4];

    //タイマー
    float timer;

    //スコア集計
    GameObject scoreCounter;
    ScoreCount sc;

    //エンドレスモード用変数
    public bool gameOver = false;

    //UI
    CanvasGroup resultGroup;
    public CanvasGroup pauseGroup;
    CanvasGroup hpBarGroup;
    public Text titleText, stageText, scoreText, timeText, hitText;
    int stageScore = 0, stageHit = 0;
    //背景
    public GameObject backGround;
    public Sprite[] backGroundSprites;

    //カットイン関係
    public GameObject cutInCanvas;
    Animator animator;
    public Sprite[] cutImage;

    //BGM関係
    AudioSource audioSource;
    public AudioClip[] audioClips;
    float fadeInSeconds = 1.0f, fadeOutSeconds = 1.0f, fadeDeltaTime, fadeDeltaTime2;
    bool isFadeIn = false, isFadeOut = false;

    //Startより早く計算される
    void Awake()
    {
        //難易度設定
        difficulty = PlayerPrefs.GetInt("difficulty");
        endless = PlayerPrefs.GetInt("endless");

        //debug用モード仮設定
        //difficulty = 0;
        //endless = 1;

        Debug.Log("mode:"+difficulty + "," + endless+","+PlayerPrefs.GetInt("gambling"));
    }

    void Start()
    {
        //クラス作成
        enemyApp =gameObject.AddComponent<enemyAppearance>();

        //hpバー取得
        if(endless==0)
        {
            hpBarGroup = GameObject.Find("StoryHP").GetComponent<CanvasGroup>();
        }
        else
        {
            hpBarGroup = GameObject.Find("EndlessHP").GetComponent<CanvasGroup>();
        }

        //hpバー消去
        hpBarGroup.alpha = 0f;

        //スコア集計＆表示
        scoreCounter = GameObject.Find("ScoreCounter");
        sc = scoreCounter.GetComponent<ScoreCount>();

        //カーソル固定
        Cursor.lockState = CursorLockMode.Locked;

        //リザルト消去
        if (endless==0)
        {
            CanvasGroup cg = GameObject.Find("PauseCanvas/TotalResult").GetComponent<CanvasGroup>();
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
            resultGroup = GameObject.Find("PauseCanvas/Result").GetComponent<CanvasGroup>();
        }
        else
        {
            CanvasGroup cg = GameObject.Find("PauseCanvas/Result").GetComponent<CanvasGroup>();
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
            resultGroup = GameObject.Find("PauseCanvas/TotalResult").GetComponent<CanvasGroup>();
        }

        //リザルト消去
        resultGroup.alpha = 0f;
        resultGroup.interactable = false;
        resultGroup.blocksRaycasts = false;

        //ポーズ画面消去
        pauseGroup.alpha = 0f;
        pauseGroup.interactable = false;
        pauseGroup.blocksRaycasts = false;

        //自機取得
        player = GameObject.Find("Player");
        pl = player.GetComponent<player>();

        if(endless==0)
        {
            //Debug用ステージスキップ ステージ数-1で入力
            stageNum = 2;

            //カットイン設定
            animator = cutInCanvas.GetComponent<Animator>();

            StartCoroutine(StoryStageStart());
        }
        else
        {
            StartCoroutine(EndlessStageStart());
        }
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        if(endless==0)
        {
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
                        Debug.Log(csvDatas[i][0]+csvDatas[i][1]+"はcsvの記述が不適切");
                    }
                    csvDatas[i][5]="1";
                }
            }
        }

        //BGMフェードイン
        if (isFadeIn)
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeInSeconds)
            {
                fadeDeltaTime = fadeInSeconds;
                isFadeIn = false;
            }
            audioSource.volume = (float)(fadeDeltaTime / fadeInSeconds);
        }
        else
        {
            fadeDeltaTime = 0;
        }

        //BGMフェードアウト
        if (isFadeOut)
        {
            fadeDeltaTime2 += Time.deltaTime;
            if (fadeDeltaTime2 >= fadeOutSeconds)
            {
                fadeDeltaTime2 = fadeOutSeconds;
                isFadeOut = false;
                //音楽を変更して再生
                audioSource.clip = audioClips[1];
                audioSource.Play();
                isFadeIn = true;
            }
            audioSource.volume = (float)(1.0 - fadeDeltaTime2 / fadeOutSeconds);
        }
        else
        {
            fadeDeltaTime2 = 0;
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

        //音楽をタイトルのものに変更
        isFadeOut = true;

        //敵消去
        //タグつきを全て格納
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys)
        {
            Destroy(del);
        }
        enemys = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject del in enemys)
        {
            Destroy(del);
        }

        //リザルト表示
        resultGroup.alpha = 1f;
        resultGroup.interactable = true;
        resultGroup.blocksRaycasts = true;

        //トータルリザルト画面にステージごとのスコアを作成
        if (gameOver && PlayerPrefs.GetInt("gambling") == 1) 
        {
            sc.SetTotalResult(stageNum, 0, Mathf.Floor(sc.GetTime()[0] * 100) / 100);
        }
        else
        {
            sc.SetTotalResult(stageNum, sc.GetScore() - stageScore, Mathf.Floor(sc.GetTime()[0] * 100) / 100);
        }

        //リザルト画面にスコア表示
        if (endless==0)
        {
            //トータルリザルトのタイトル変更
            titleText.text = "Conquer All Stages!";
            stageText.text = "Stage-" + stageNum;
            scoreText.text = "Score:" + (sc.GetScore() - stageScore);
            timeText.text = "Time:" + (Mathf.Floor(sc.GetTime()[0] * 100) / 100);
            hitText.text = "Hit:" + (pl.hitNum - stageHit);
            stageScore = sc.GetScore();
            stageHit = pl.hitNum;
        }
        else
        {
            if(gameOver)
            {
                //文字変更
                titleText.text = "Game Over";
                //色変更：赤
                titleText.color = Color.red;
                //フォント変更：明朝体
                Font font = Resources.Load<Font>("Font/brelaregular");
                titleText.font = font;
                //bold解除
                titleText.fontStyle = FontStyle.Normal;
                //位置調整
                titleText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,172,0);
            }
            stageScore = sc.GetScore();
            stageHit = pl.hitNum;
        }
    }

    public IEnumerator StoryStageStart()
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
        resultGroup.blocksRaycasts = false;

        //ポーズ画面消去
        pauseGroup.alpha = 0f;
        pauseGroup.interactable = false;
        pauseGroup.blocksRaycasts = false;

        //背景設定
        backGround.GetComponent<Image>().sprite = backGroundSprites[stageNum];

        //BGM設定
        GameObject mainAudio = GameObject.Find("AudioObj");
        audioSource = mainAudio.GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.volume = 0;
        audioSource.Play();
        isFadeIn = true;

        stageNum++;

        //敵情報をDBから受け取る
        yield return StartCoroutine(jsonRec.ConnectDB(PlayerPrefs.GetString("ID")));

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

            // , で分割しつつ一行ずつ読み込み，リストに追加していく
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                csvDatas.Add(line.Split(','));
            }

            timer = 0;
            sc.ResetTimer();
            //Debug用ステージ早送り
            //timer = 60;
        }

        yield return null;
    }

    public IEnumerator EndlessStageStart()
    {
        //自機動作
        Cursor.visible = false;
        pl.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("CursorMove", 0.1f);

        //hpバー消去
        hpBarGroup.alpha = 0f;

        //リザルト消去
        resultGroup.alpha = 0f;
        resultGroup.interactable = false;
        resultGroup.blocksRaycasts = false;

        //ポーズ画面消去
        pauseGroup.alpha = 0f;
        pauseGroup.interactable = false;
        pauseGroup.blocksRaycasts = false;

        stageNum++;

        //boss呼び出し
        for (int i = 0; i < selectBoss.Length; i++)
        {
            if (selectBoss[i] == 0)
            {
                bossRan = UnityEngine.Random.Range(1, 5);
                if (i == 0)
                {
                    selectBoss[i] = bossRan;
                    break;
                }
                while (selectBoss[0] == bossRan || selectBoss[1] == bossRan || selectBoss[2] == bossRan)
                {
                    bossRan = UnityEngine.Random.Range(1, 5);
                }
                selectBoss[i] = bossRan;
                break;
            }
        }
        if (selectBoss[3] != 0)
        {
            selectBoss = new int[4];
            if(difficulty<3)
            {
                difficulty++;
            }
        }

        //背景設定
        backGround.GetComponent<Image>().sprite = backGroundSprites[bossRan - 1];

        //敵情報をDBから受け取る
        yield return StartCoroutine(jsonRec.ConnectDB(PlayerPrefs.GetString("ID")));

        csvFile = Resources.Load("CSV/bossOnly") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み，リストに追加していく
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }

        float posX = float.Parse(csvDatas[bossRan - 1][1]);
        float posY = float.Parse(csvDatas[bossRan - 1][2]);
        enemyApp.BossAppearance(bossList[bossRan - 1], posX, posY);
        hpBarGroup.alpha = 1f;

        sc.ResetTimer();

        yield return null;
    }

    void CursorMove()
    {
        //カーソルのロック解除
        Cursor.lockState = CursorLockMode.Confined;
    }

    void CutIn()
    {
        //ステージごとに背景変更
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

    public void SaveScore()
    {
        int score = sc.GetScore();

        //DB保存
        StartCoroutine(jsonRec.SaveScoreData(PlayerPrefs.GetString("ID"), endless, difficulty, score));
    }
}