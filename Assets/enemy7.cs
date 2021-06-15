using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy7 : MonoBehaviour
{
    //回転速度
    float rotSpeed;
    Vector3 enemyPlace;
    float angle,radius,speed,timer;
    int hit=0,hp=3;

    void Start()
    {
        angle=Random.Range(30f,150f);
        radius=Random.Range(5f,10f);
        rotSpeed=5f;
        speed=Random.Range(1f,3f);
        timer=0;
        if(angle%2<1){
            speed*=-1;
        }
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        enemyPlace=transform.position;
        angle+=speed;
        float rad=Mathf.PI*angle/180;
        enemyPlace.x=(float)Mathf.Cos(rad)*radius+0;
        enemyPlace.y=(float)Mathf.Sin(rad)*radius+6;
        transform.position=enemyPlace;

        transform.Rotate(0,0,rotSpeed);

        if(timer>10){
            Destroy(gameObject);
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
            sc.AddScore(7);

            Destroy(col.gameObject);
            if(hit>hp){
                Destroy(gameObject);
            }
        }
    }
}
