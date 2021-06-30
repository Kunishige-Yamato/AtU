using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy4 : MonoBehaviour
{
    public GameObject bulletPrefab;
    Vector3 bulletPlace;
    float angle;
    int hp=20;
    int hit=0;
    float timer;
    
    void Start()
    {
        gameObject.name="enemy4Prefab";
        Shoot();
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        if(timer>=10){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        angle=Random.Range(-180f,360f);
        float rad=Mathf.PI*angle/180;
        bulletPlace.x=(float)Mathf.Cos(rad)*1.5f+transform.position.x;
        bulletPlace.y=(float)Mathf.Sin(rad)*1.5f+transform.position.y;
        GameObject bul=Instantiate(bulletPrefab,bulletPlace,Quaternion.identity) as GameObject;
        bullet26 bulCom=bul.GetComponent<bullet26>();
        bulCom.enemyPrefab=gameObject;
        bulCom.parent=4;
        Invoke("Shoot",0.07f);
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
            sc.AddScore(5);

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
