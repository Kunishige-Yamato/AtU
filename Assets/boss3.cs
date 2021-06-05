using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class boss3 : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;
    public GameObject bullet3Prefab;
    public GameObject bullet4Prefab;
    public GameObject bullet5Prefab;
    Vector3 wallPlace;
    Vector3 bulletPlace;
    Vector3 bullet2Place;
    Vector3 bullet3Place;
    Vector3 bullet4Place;
    Vector3 bullet5Place;
    Vector3 objPlace;
    int hp=400;
    int hit=0;
    int mode=0;
    float moveSpeed;
    float angle=0,posX=-8;
    int count,count2,count3;

    float timer,lastTime;

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas=new List<string[]>(); // CSVの中身を入れるリスト;

    void Start()
    {
        timer=0;

        //csv読み込み
        csvFile=Resources.Load("boss-3") as TextAsset;

        addList();        
    }

    void addList()
    {
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
            if(float.Parse(csvDatas[i][1])+lastTime<=timer&&csvDatas[i][2]=="0"){
                Generate(csvDatas[i][0]);
                if(csvDatas[i][0]!="end"){
                    csvDatas[i][2]="1";
                }
                else{
                    lastTime+=float.Parse(csvDatas[i][1]);
                }
            }
        }
    }

    void Defense()
    {
        for(float i=-1.5f;i<=1.5f;i+=1.5f){
            wallPlace.x=transform.position.x+i;
            wallPlace.y=transform.position.y-2;
            Instantiate(wallPrefab,wallPlace,Quaternion.identity);
        }
    }

    void Shoot1()
    {
        bulletPlace.x=0;
        bulletPlace.y=5.8f;
        Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
    }

    void Shoot2()
    {
        if(mode==0){
            //ボス移動
            moveSpeed=0.01f;
            objPlace=transform.position;
            objPlace.y-=moveSpeed;
            transform.position=objPlace;
            if(transform.position.y<0){
                mode=1;
            }
        }
        if(mode==1){
            moveSpeed=0;
            objPlace.x=0;
            objPlace.y=0;
            transform.position=objPlace;
            mode=2;
        }
        if(mode==2){
            if(angle<720){
                float rad=Mathf.PI*(angle+90)/180;
                bullet2Place.x=(float)Mathf.Cos(rad)*2+transform.position.x;
                bullet2Place.y=(float)Mathf.Sin(rad)*2+transform.position.y;
                angle+=0.8f;
                Instantiate(bullet2Prefab,bullet2Place,Quaternion.identity);
            }
            else{
                angle=0;
                mode=3;
            }
        }
        if(mode==3){
            //ボス移動
            moveSpeed=0.01f;
            objPlace=transform.position;
            objPlace.y+=moveSpeed;
            transform.position=objPlace;
            if(transform.position.y>3){
                mode=4;
            }
        }
        if(transform.position.y>3&&mode==4){
            moveSpeed=0;
            objPlace.x=0;
            objPlace.y=3;
            transform.position=objPlace;
            mode=5;
        }
        if(mode==5){
            mode=0;
        }
        else{
            Invoke("Shoot2",0.01f);
        }
    }

    void Shoot3()
    {
        count++;
        for(float i=-5;i<11;i+=2){
            bullet3Place.y=i;
            bullet3Place.x=-9.5f;
            Instantiate(bullet3Prefab,bullet3Place,Quaternion.identity);
            bullet3Place.x=9.5f;
            Instantiate(bullet3Prefab,bullet3Place,Quaternion.identity);
        }
        if(count<5){
            Invoke("Shoot3",0.8f);
        }
        else{
            count=0;
        }
    }

    void Shoot4()
    {
        count2++;

        bullet4Place.x=Random.Range(-9f,9f);
        bullet4Place.y=6f;
        Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
        bullet4Place.x=9.5f;
        bullet4Place.y=Random.Range(-5f,5f);
        Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
        bullet4Place.x=-9.5f;
        bullet4Place.y=Random.Range(-5f,5f);
        Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);

        if(count2<100){
            Invoke("Shoot4",0.4f);
        }
        else{
            count2=0;
        }
    }

    void Shoot5()
    {
        if(posX>8){
            posX=-8;
        }
        bullet5Place.x=posX;
        bullet5Place.y=3;
        Instantiate(bullet5Prefab,bullet5Place,Quaternion.identity);

        count3++;
        posX+=0.5f;

        //周回数
        int rap=5;
        if(count3<33*rap){
            Invoke("Shoot5",0.03f);
        }
        else{
            count3=0;
        }
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
            case "w1":
                Defense();
                break;
            case "s1":
                Shoot1();
                break;
            case "s2":
                Shoot2();
                break;
            case "s3":
                Shoot3();
                break;
            case "s4":
                Shoot4();
                break;
            case "s5":
                Shoot5();
                break;
            case "end":
                addList();
                break;
        }
    }
}
