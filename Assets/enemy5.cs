using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy5 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    public GameObject bulletPrefab;
    Vector3 bulletPlace;
    float angle;
    int hp=5;
    int hit=0;
    
    void Start()
    {
        gameObject.name="enemy5Prefab";
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(0,-fallSpeed,0,Space.World);
        fallSpeed+=0.0005f;
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        for(int i=0;i<360;i+=15){
            angle=i;
            float rad=Mathf.PI*angle/180;
            bulletPlace.x=(float)Mathf.Cos(rad)*0.5f+transform.position.x;
            bulletPlace.y=(float)Mathf.Sin(rad)*0.5f+transform.position.y;
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
        }
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
            sc.AddScore(20);

            Destroy(col.gameObject);
            if(hit>hp){
                Shoot();
                Destroy(gameObject);
            }
        }
    }
}
