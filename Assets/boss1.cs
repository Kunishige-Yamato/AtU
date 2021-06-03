using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class boss1 : MonoBehaviour
{
    int hp=400;
    int hit=0;
    float mov,maxMov,minMov;
    public bool dir;
    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;
    public GameObject bullet3Prefab;
    Vector3 bulletPlace,bulletPlace2;
    public int num=0,num2=0;

    float timer;

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas=new List<string[]>(); // CSVの中身を入れるリスト;

    void Start()
    {
        bulletPlace.x=gameObject.transform.position.x;
        bulletPlace.y=gameObject.transform.position.y-2;

        mov=-1;
        maxMov=mov*-1;
        minMov=mov;
        dir=false;

        timer=0;

        //csv読み込み
        csvFile=Resources.Load("boss-1") as TextAsset;
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
            if(float.Parse(csvDatas[i][1])<=timer&&csvDatas[i][2]=="0"){
                Generate(csvDatas[i][0]);
                csvDatas[i][2]="1";
            }
        }
    }

    void Shoot1()
    {
        for(float i=-2;i<=2;i+=0.1f)
        {
            GameObject mb=Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
            mb.name="bullet_"+num;
            GameObject bul=GameObject.Find("bullet_"+num);
            num++;
            bullet8 b=bul.GetComponent<bullet8>();
            b.moveSpeed=i;
        }
    }

    void Shoot2()
    {
        if(mov<=maxMov&&dir==false){
            GameObject mb2=Instantiate(bullet2Prefab,bulletPlace,Quaternion.identity);
            mb2.name="bullet_"+num2;
            GameObject bul2=GameObject.Find("bullet_"+num2);
            num2++;
            bullet9 b2=bul2.GetComponent<bullet9>();
            b2.moveSpeed=mov/10;
            mov+=0.03f;
            Invoke("Shoot2",0.06f);
        }
        else if(dir==false)
        {
            Invoke("switchDir",1);
        }
        if(mov>=minMov&&dir==true){
            GameObject mb2=Instantiate(bullet2Prefab,bulletPlace,Quaternion.identity);
            mb2.name="bullet_"+num2;
            GameObject bul2=GameObject.Find("bullet_"+num2);
            num2++;
            bullet9 b2=bul2.GetComponent<bullet9>();
            b2.moveSpeed=mov/10;
            mov-=0.03f;
            Invoke("Shoot2",0.06f);
        }
    }

    void switchDir()
    {
        dir=true;
        Shoot2();
    }

    void Shoot3()
    {
        bulletPlace2.x=Random.Range(-8f,8f);
        bulletPlace2.y=5.5f;
        Invoke("Shoot3",Random.Range(0.1f,0.3f));
        Instantiate(bullet3Prefab,bulletPlace2,Quaternion.identity);
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Bullet")
        {
            hit++;
            if(hit>hp)
            {
                Destroy(gameObject);
            }
        }
    }

    void Generate(string n)
    {   
        switch(n){
            case "s1":
                Shoot1();
                break;
            case "s2":
                Shoot2();
                break;
            case "s3":
                Shoot3();
                break;
        }
    }
}
