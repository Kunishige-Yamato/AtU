using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

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
    float timer;

    void Start()
    {
        ReadFile();
    }

    public void ReadFile()
    {
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

        //もしステージクリア毎にリザルト挟むならリザルトをボスが呼んでからリザルトがReadFile()呼ぶ
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
                Instantiate(boss1Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "boss2":
                Instantiate(boss2Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "boss3":
                Instantiate(boss3Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
            case "boss4":
                Instantiate(boss4_1Prefab,new Vector3(x,y,0),Quaternion.identity);
                Instantiate(boss4_2Prefab,new Vector3(x,y,0),Quaternion.identity);
                break;
        }
    }
}
