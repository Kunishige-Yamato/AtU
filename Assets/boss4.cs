using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class boss4 : MonoBehaviour
{
    GameObject cakePrefab;
    boss4 cakeCom;

    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;
    public GameObject bullet3Prefab;
    public GameObject bullet4Prefab;
    public GameObject bullet5Prefab;
    public GameObject bullet6Prefab;
    Vector3 bulletPlace;
    Vector3 bullet2Place;
    Vector3 bullet3Place;
    Vector3 bullet4Place;
    Vector3 bullet5Place;
    Vector3 bullet6Place;
    int hp=1000;
    public int hit=0;
    int side;
    float angle;
    int count=0,count2=0,count3=0;
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
        eg=GameObject.Find("EG");
        enemyGenerator=eg.GetComponent<EnemyGenerator>();

        timer=0;

        //hpバー制御
        hpBar=GameObject.Find("Slider").GetComponent<Slider>();
        hpBar.maxValue=hp;
        hpBar.value=hp;

        //csv読み込み
        csvFile=Resources.Load("boss-4") as TextAsset;
        StringReader reader=new StringReader(csvFile.text);

        addList();     
    }

    void Clear()
    {
        //タグつきを全て格納
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys) {
            if(del!=gameObject&&del!=cakePrefab){
                Destroy(del);
            }
        }
    }

    void findCake()
    {
        if(transform.position.x>0){
            gameObject.name="boss4-2Prefab";
            side=1;
            cakePrefab=GameObject.Find("boss4-1Prefab");
        }
        else{
            gameObject.name="boss4-1Prefab";
            side=-1;
            cakePrefab=GameObject.Find("boss4-2Prefab");
        }
        if(cakePrefab!=null){
            cakeCom=cakePrefab.GetComponent<boss4>();
            Clear();
        }
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

        if(cakePrefab==null){
            findCake();
        }
        else if(cakeCom.hit>=this.hit){
            this.hit=cakeCom.hit;
        }

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
        for(float i=-4f;i<6;i+=3){
            bulletPlace.x=9.5f*side;
            bulletPlace.y=i;
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        }
    }

    void Shoot2()
    {
        angle=Random.Range(-180f,360f);
        float rad=Mathf.PI*angle/180;
        bullet2Place.x=(float)Mathf.Cos(rad)*2+transform.position.x;
        bullet2Place.y=(float)Mathf.Sin(rad)*2+transform.position.y;
        Instantiate(bullet2Prefab,bullet2Place,Quaternion.identity);
        count++;
        if(count<150){
            Invoke("Shoot2",0.08f);
        }
        else{
            count=0;
        }
    }

    void Shoot3()
    {
        if(side<0){
            bullet3Place.x=Random.Range(-5f,0);
        }
        else{
            bullet3Place.x=Random.Range(0,5f);
        }
        bullet3Place.y=7.5f;
        GameObject　go=Instantiate(bullet3Prefab,bullet3Place,Quaternion.identity) as GameObject;
        go.name="bullet23-1("+side+")";
    }

    void Shoot4()
    {
        if(transform.position.x>0){
            int i=Random.Range(-4,5)*2;
            for(int j=-8;j<=8;j+=2){
                bullet4Place.x=j;
                bullet4Place.y=6f;
                if(i!=j){
                    Instantiate(bullet4Prefab,bullet4Place,Quaternion.identity);
                }
            }
            count2++;
            if(count2<6){
                Invoke("Shoot4",1.5f);
            }
            else{
                count2=0;
            }
        }

    }

    void Shoot5()
    {
        Instantiate(bullet5Prefab,transform.position,Quaternion.identity);
        count3++;
        if(count3<75){
            Invoke("Shoot5",0.15f);
        }
        else{
            count3=0;
        }
    }

    void Shoot6()
    {
        bullet6Place.x=0;
        bullet6Place.y=4.5f;
        if(side>0){
            Instantiate(bullet6Prefab,bullet6Place,Quaternion.identity);
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
            if(hit%3==0){
                Instantiate(bullet5Prefab,transform.position,Quaternion.identity);
            }
            if(hit>this.hp*0.75&&hit%3==0){
                Instantiate(bullet5Prefab,transform.position,Quaternion.identity);
            }

            Destroy(col.gameObject);
            if(hit>this.hp)
            {
                //爆発
		        Instantiate (explosionPrefab, transform.position, Quaternion.identity);
                //早期撃退ボーナス
                sc.AddScore((int)Mathf.Floor(12000/timer));
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
            case "s6":
                Shoot6();
                break;
            case "end":
                addList();
                break;
        }
    }
}
