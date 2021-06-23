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

    public GameObject boss1Prefab;
    public GameObject boss2Prefab;
    public GameObject boss3Prefab;
    public GameObject boss4_1Prefab;
    public GameObject boss4_2Prefab;

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas=new List<string[]>(); // CSVの中身を入れるリスト;

    int stageNum=0;
    int allStageNum=4;
    float timer,sumTime;

    GameObject player;
    player pl;

    public CanvasGroup resultGroup;
    public Text stageText;
    public Text scoreText;
    public Text timeText;
    public Text hitText;
    public Text nextButtonText;

    void Start()
    {
        player=GameObject.Find("Player");
        pl=player.GetComponent<player>();

        sumTime=0;

        ReadFile();
    }

    public void DisplayResult()
    {
        //自機停止
        pl.enabled=false;
        
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
        scoreText.text="Score:"+sc.returnScore();
        sumTime+=timer;
        timeText.text="Time:"+(Mathf.Floor(sumTime*100)/100);
        hitText.text="Hit:"+pl.hitNum;
    }

    public void ReadFile()
    {
        //自機停止
        pl.enabled=true;

        //リザルト表示
        resultGroup.alpha=0f;
        resultGroup.interactable=false;

        stageNum++;

        if(stageNum<=allStageNum){
            //csv読み込み
            csvFile=Resources.Load("stage-"+stageNum) as TextAsset;
            StringReader reader=new StringReader(csvFile.text);

            // , で分割しつつ一行ずつ読み込み，リストに追加していく
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                string line=reader.ReadLine(); // 一行ずつ読み込み
                csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
            }

            timer=0;
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
            case "boss1":
                Instantiate(boss1Prefab,new Vector3(0,3,0),Quaternion.identity);
                break;
            case "boss2":
                Instantiate(boss2Prefab,new Vector3(0,3,0),Quaternion.identity);
                break;
            case "boss3":
                Instantiate(boss3Prefab,new Vector3(0,3,0),Quaternion.identity);
                break;
            case "boss4":
                Instantiate(boss4_1Prefab,new Vector3(-5,3,0),Quaternion.identity);
                Instantiate(boss4_2Prefab,new Vector3(5,3,0),Quaternion.identity);
                break;
        }
    }
}
