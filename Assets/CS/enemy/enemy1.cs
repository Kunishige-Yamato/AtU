using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //回転速度
    float rotSpeed;
    public GameObject bulletPrefab;
    Vector3 bulletPlace,objPlace;
    int direction;
    int hit=0;
    int hp=5;

    void Start()
    {
        objPlace=transform.position;
        this.fallSpeed=0;
        this.rotSpeed=10f;
        if(gameObject.transform.position.x>=0)
        {
            bulletPlace.x=gameObject.transform.position.x-1;
        }
        else
        {
            bulletPlace.x=gameObject.transform.position.x+1;
        }
        bulletPlace.y=gameObject.transform.position.y;
        bulletPlace.z=gameObject.transform.position.z;
        Shoot();
        gameObject.transform.position=objPlace;
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(0,-fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);
        bulletPlace.y=gameObject.transform.position.y;
        fallSpeed+=0.002f;
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Invoke("Shoot",0.3f);
        Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
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
                Destroy(gameObject);
            }
        }
    }
}
