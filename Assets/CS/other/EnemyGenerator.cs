using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;
    public GameObject enemy5Prefab;
    public GameObject enemy6Prefab;
    public GameObject enemy7Prefab;
    public GameObject enemy8Prefab;
    public GameObject enemy9Prefab;

    public GameObject bullet3Prefab;
    public GameObject bullet4Prefab;
    public GameObject bullet5Prefab;
    public GameObject bullet6Prefab;
    public GameObject bullet23Prefab;

    public GameObject boss1Prefab;
    public GameObject boss2Prefab;
    public GameObject boss3Prefab;
    public GameObject boss4_1Prefab;
    public GameObject boss4_2Prefab;

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas=new List<string[]>(); // CSVの中身を入れるリスト;

    public int stageNum=0;
    public int allStageNum=4;
    public float timer,sumTime;

    GameObject player;
    player pl;

    public CanvasGroup resultGroup;
    public CanvasGroup pauseGroup;
    public CanvasGroup hpBarGroup;
    public Text stageText;
    public Text scoreText;
    public Text timeText;
    public Text hitText;
    int stageScore=0,stageHit=0;

    // 位置座標
	private Vector3 mousePosition;
	// スクリーン座標をワールド座標に変換した位置座標
	private Vector3 screenToWorldPointPosition;

    CursorLockMode wantedMode = CursorLockMode.None;

    void Start()
    {
        player=GameObject.Find("Player");
        pl=player.GetComponent<player>();

        sumTime=0;

        //テスト用ステージスキップ
        stageNum=3;

        // 初期動作
        Cursor.lockState=wantedMode;
        Cursor.lockState=wantedMode=CursorLockMode.Confined;
        Cursor.visible=false; 

        ReadFile();
    }

    public void DisplayResult()
    {
        //自機停止
        pl.canMove=false;

        //カーソル表示
        Cursor.visible=true; 

        //hpバー消去
        hpBarGroup.alpha=0f;
        
        //敵消去
        //タグつきを全て格納
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys) {
            Destroy(del);
        }

        //リザルト表示
        resultGroup.alpha=1f;
        resultGroup.interactable=true;

        //スコア集計＆表示
        stageText.text="Stage-"+stageNum;
        GameObject scoreCounter=GameObject.Find("ScoreCounter");
        ScoreCount sc=scoreCounter.GetComponent<ScoreCount>();
        scoreText.text="Score:"+(sc.returnScore()-stageScore);
        stageScore=sc.returnScore();
        timeText.text="Time:"+(Mathf.Floor(timer*100)/100);
        sumTime+=timer;
        hitText.text="Hit:"+(pl.hitNum-stageHit);
        stageHit=pl.hitNum;
    }

    public void ReadFile()
    {
        //自機動作
        Cursor.visible=false;
        pl.canMove=true;

        //hpバー消去
        hpBarGroup.alpha=0f;

        //リザルト消去
        resultGroup.alpha=0f;
        resultGroup.interactable=false;

        //ポーズ画面消去
        pauseGroup.alpha=0f;
        pauseGroup.interactable=false;

        stageNum++;

        if(stageNum<=allStageNum){
            //csv読み込み
            switch(selectDifficulty.difficulty){
                case 0:
                    csvFile=Resources.Load("stage-e-"+stageNum) as TextAsset;
                    break;
                case 1:
                    csvFile=Resources.Load("stage-n-"+stageNum) as TextAsset;
                    break;
                case 2:
                    csvFile=Resources.Load("stage-h-"+stageNum) as TextAsset;
                    break;
                case 3:
                    csvFile=Resources.Load("stage-c-"+stageNum) as TextAsset;
                    break;
            }
            StringReader reader=new StringReader(csvFile.text);

            //テスト用ボスのみ出現
            //string boss="boss"+stageNum;
            //Generate(boss,0,3);


            // , で分割しつつ一行ずつ読み込み，リストに追加していく
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                string line=reader.ReadLine(); // 一行ずつ読み込み
                csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
            }

            timer=0;

            Debug.Log("stage:"+stageNum);
        }
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        for(int i=0;i<csvDatas.Count;i++){
            if(float.Parse(csvDatas[i][1])<=timer&&csvDatas[i][4]=="0"){
                Generate(csvDatas[i][0],float.Parse(csvDatas[i][2]),float.Parse(csvDatas[i][3]));
                csvDatas[i][4]="1";
            }
        }
    }

    void Generate(string n,float x,float y)
    {   
        switch(n){
            case "e1":
                Instantiate(enemy1Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e2":
                Instantiate(enemy2Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e3":
                Instantiate(enemy3Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e4":
                Instantiate(enemy4Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e5":
                Instantiate(enemy5Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e6":
                Instantiate(enemy6Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e7":
                Instantiate(enemy7Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e8":
                Instantiate(enemy8Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "e9":
                Instantiate(enemy9Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "b3":
                Instantiate(bullet3Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "b4":
                Instantiate(bullet4Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "b5":
                Instantiate(bullet5Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "b6":
                Instantiate(bullet6Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "b23":
                GameObject　go=Instantiate(bullet23Prefab,new Vector3(2,8,0),Quaternion.identity) as GameObject;
                go.name="bullet23-1(1)";
                GameObject　go2=Instantiate(bullet23Prefab,new Vector3(-2,8,0),Quaternion.identity) as GameObject;
                go2.name="bullet23-1(-1)";
                break;
            case "boss1":
                Instantiate(boss1Prefab,new Vector3(0,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
            case "boss2":
                Instantiate(boss2Prefab,new Vector3(0,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
            case "boss3":
                Instantiate(boss3Prefab,new Vector3(0,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
            case "boss4":
                Instantiate(boss4_1Prefab,new Vector3(-5,3,0),Quaternion.identity);
                Instantiate(boss4_2Prefab,new Vector3(5,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
        }
    }
}
