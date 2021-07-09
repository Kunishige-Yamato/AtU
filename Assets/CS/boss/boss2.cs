using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class boss2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;
    public GameObject bullet3Prefab;
    public GameObject bullet4Prefab;
    public GameObject bullet5Prefab;
    Vector3 bulletPlace;
    Vector3 bullet2Place;
    Vector3 bullet3Place;
    Vector3 bullet4Place;
    Vector3 bullet5Place;
    int count=0;
    int count2=0;
    int count3=0;
    bool direction;
    Vector3 objPlace;
    int hp=500;
    int hit=0;
    GameObject eg;
    EnemyGenerator enemyGenerator;
    Slider hpBar;

    //爆発エフェクトのPrefab
	public GameObject explosionPrefab;

    float timer,lastTime;

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas=new List<string[]>(); // CSVの中身を入れるリスト;

    void Start()
    {
        //タグつきを全て格納
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys) {
            if(del!=gameObject){
                Destroy(del);
            }
        }

        eg=GameObject.Find("EG");
        enemyGenerator=eg.GetComponent<EnemyGenerator>();
        
        timer=0;

        //hpバー制御
        hpBar=GameObject.Find("Slider").GetComponent<Slider>();
        hpBar.maxValue=hp;
        hpBar.value=hpBar.maxValue;

        //csv読み込み
        csvFile=Resources.Load("boss-2") as TextAsset;

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

        float moveSpeed=0.01f;
        objPlace=transform.position;
        if(objPlace.x<4&&direction==true){
            objPlace.x+=moveSpeed;
        }
        else{
            direction=false;
        }
        if(objPlace.x>-4&&direction==false){
            objPlace.x-=moveSpeed;
        }
        else{
            direction=true;
        }
        transform.position=objPlace;

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

    void Shoot1()
    {
        bulletPlace.x=Random.Range(-9,9);
        bulletPlace.y=5.5f;
        Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        if(count<80){
            Invoke("Shoot1",0.1f);
            count++;
        }
        else{
            count=0;
        }
    }

    void Shoot2()
    {
        for(int i=-8;i<=8;i+=2){
            bullet2Place.x=i;
            bullet2Place.y=5.5f;
            Instantiate(bullet2Prefab,bullet2Place,Quaternion.identity);
        }
    }

    void Shoot3()
    {
        for(float i=-6;i<=2;i+=1.5f){
            bullet3Place.x=-9.5f;
            bullet3Place.y=i;
            Instantiate(bullet3Prefab,bullet3Place,Quaternion.identity);
        }
        if(count2<2){
            Invoke("Shoot3",3.8f);
            count2++;
        }
        else{
            count2=0;
        }

    }

    void Shoot4()
    {
        for(float i=-5.1f;i<5;i+=2){
            bullet4Place.x=-9.5f;
            bullet4Place.y=i;
            Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
        }
        for(float i=-9f;i<9;i+=2f){
            bullet4Place.x=i;
            bullet4Place.y=5.5f;
            Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
        }
    }

    void Shoot5()
    {
        bullet5Place.x=Random.Range(-8f,8f);
        bullet5Place.y=-5.5f;
        Instantiate(bullet5Prefab,bullet5Place,Quaternion.identity);
        if(count3<100){
            Invoke("Shoot5",0.1f);
            count3++;
        }
        else{
            count3=0;
        }
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Bullet"&&timer>0.1f)
        {
            hit++;
            
            hpBar.value=hp-hit;

            //スコア付与
            GameObject scoreCounter=GameObject.Find("ScoreCounter");
            ScoreCount sc=scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(10);

            bullet0 bul0=col.GetComponent<bullet0>();
            bul0.explosion();
            Destroy(col.gameObject);
            
            if(hit>hp)
            {
                //爆発
		        Instantiate (explosionPrefab, transform.position, Quaternion.identity);
                //早期撃退ボーナス
                sc.AddScore((int)Mathf.Floor(15000/timer));
                //次のステージへ
                enemyGenerator.DisplayResult();
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
