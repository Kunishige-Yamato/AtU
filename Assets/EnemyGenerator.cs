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

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas=new List<string[]>(); // CSVの中身を入れるリスト;

    float timer;

    void Start()
    {
        timer=0;

        //csv読み込み
        csvFile=Resources.Load("stage-1") as TextAsset;
        StringReader reader=new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み，リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line=reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        for(int i=0;i<csvDatas.Count;i++){
            if(float.Parse(csvDatas[i][1])<=timer&&csvDatas[i][4]=="0"){
                //Generate(csvDatas[i][0],float.Parse(csvDatas[i][2]),float.Parse(csvDatas[i][3]));
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
        }
    }
}
