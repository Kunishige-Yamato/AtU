using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy6 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //回転速度
    float rotSpeed;
    public GameObject bulletPrefab;
    Vector3 bulletPlace;
    Vector3 bulletScale;
    float angle;
    int hp=3;
    int hit=0;
    bool stop=false,explosion=false;
    
    void Start()
    {
        rotSpeed=10;
        gameObject.name="enemy6Prefab";
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(0,-fallSpeed,0,Space.World);
        fallSpeed+=0.0005f;
        //下まで行ったら消去
        if(transform.position.y<-6.5f&&stop==false){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        stop=true;
        if(fallSpeed>0){
            //ブレーキ
            fallSpeed-=0.003f;
            //拡大
            bulletScale=transform.localScale;
            bulletScale.x+=0.005f;
            bulletScale.y+=0.005f;
            transform.localScale=bulletScale;
            Invoke("Shoot",0.05f);
            //回転
            transform.Rotate(0,0,rotSpeed);
        }
        else{
            fallSpeed=0;
            for(int i=0;i<360;i+=12){
                angle=i;
                float rad=Mathf.PI*angle/180;
                bulletPlace.x=(float)Mathf.Cos(rad)*0.5f+transform.position.x;
                bulletPlace.y=(float)Mathf.Sin(rad)*0.5f+transform.position.y;
                GameObject bul=Instantiate(bulletPrefab,bulletPlace,Quaternion.identity) as GameObject;
                bullet26 bulCom=bul.GetComponent<bullet26>();
                bulCom.enemyPrefab=gameObject;
                bulCom.parent=6;
                Destroy(gameObject);
            }
        }
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Bullet"&&transform.position.y<5.5f)
        {
            hit++;

            //スコア付与
            GameObject scoreCounter=GameObject.Find("ScoreCounter");
            ScoreCount sc=scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(20);

            bullet0 bul0=col.GetComponent<bullet0>();
            bul0.explosion();
            Destroy(col.gameObject);
            
            if(hit>hp&&explosion==false){
                Shoot();
                explosion=true;
            }
        }
    }
}
