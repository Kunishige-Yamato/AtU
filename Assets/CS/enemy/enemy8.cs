using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy8 : MonoBehaviour
{
    public GameObject bulletPrefab;
    Vector3 bulletPlace,bullet2Place;
    float angle,angle2;
    int hp=20;
    int hit=0;
    float timer;
    
    void Start()
    {
        gameObject.name="enemy8Prefab";
        angle=0;
        angle2=180;
        Shoot();
        Shoot2();
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        if(timer>=12){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        for(int i=0;i>-25;i-=5){
            angle+=i;
            float rad=Mathf.PI*angle/180;
            bulletPlace.x=(float)Mathf.Cos(rad)*1.5f+transform.position.x;
            bulletPlace.y=(float)Mathf.Sin(rad)*1.5f+transform.position.y;
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        }
        if(angle>-180){
            angle+=15;
        }
        else{
            angle=0;
        }
        Invoke("Shoot",0.3f);
    }

    void Shoot2()
    {
        for(int i=0;i<5;i++){
            angle2+=i;
            float rad=Mathf.PI*angle2/180;
            bullet2Place.x=(float)Mathf.Cos(rad)*0.8f+transform.position.x;
            bullet2Place.y=(float)Mathf.Sin(rad)*0.8f+transform.position.y;
            Instantiate(bulletPrefab,bullet2Place,Quaternion.identity);
        }
        if(angle2<360){
            angle2+=15;
        }
        else{
            angle2=180;
        }
        Invoke("Shoot2",0.8f);
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Bullet")
        {
            hit++;

            //スコア付与
            GameObject scoreCounter=GameObject.Find("ScoreCounter");
            ScoreCount sc=scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(10);

            bullet0 bul0=col.GetComponent<bullet0>();
            bul0.explosion();
            Destroy(col.gameObject);
            
            if(hit>hp){
                //早期撃退ボーナス
                sc.AddScore((int)Mathf.Floor(100/timer));
                Destroy(gameObject);
            }
        }
    }
}
