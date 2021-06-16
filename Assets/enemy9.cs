using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy9 : MonoBehaviour
{
    public GameObject bulletPrefab;
    GameObject player;
    Vector3 bulletPlace;
    float angle;
    int hp=70;
    int hit=0;
    float timer;
    
    void Start()
    {
        gameObject.name="enemy9Prefab";
        player=GameObject.Find("Player");
        angle=0;
        Shoot();
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        if(timer>=60){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        angle=0;
        int i=Random.Range(0,24);
        for(int j=0;j<24;j++){
            angle=j*15;
            float rad=Mathf.PI*angle/180;
            bulletPlace.x=(float)Mathf.Cos(rad)*3f+player.transform.position.x;
            bulletPlace.y=(float)Mathf.Sin(rad)*3f+player.transform.position.y;
            if(i!=j&&(i-1)!=j&&(i+1)!=j){
                Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
            }
        }
        Invoke("Shoot",4f);
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
            sc.AddScore(30);

            Destroy(col.gameObject);
            if(hit>hp){
                //早期撃退ボーナス
                sc.AddScore((int)Mathf.Floor(200/timer));
                Destroy(gameObject);
            }
        }
    }
}
