using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet16_1 : MonoBehaviour
{
    //落下速度
    float fallSpeed=0;
    //横移動速度
    public float moveSpeed=0;
    //bullet14-2呼び出し
    public GameObject bulletPrefab;
    Vector3 bulletPlace;
    float timer;
    GameObject pl;
    public bool del=false;

    void Start()
    {
        fallSpeed=0;
        moveSpeed=0;
        timer=0;
        pl=GameObject.Find("Player");
        Invoke("Delete",30f);
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;
        bulletPlace=transform.position;

        //置き土産
        if(timer>0.3f){
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
            timer=0;
        }

        //自身の移動
        transform.Translate(moveSpeed,fallSpeed,0,Space.World);

        //自機狙い
        float speedX=0.004f;
        float speedY=0.002f;
        if(pl.transform.position.y>gameObject.transform.position.y){
            fallSpeed+=speedY;
        }
        else if(pl.transform.position.y<gameObject.transform.position.y){
            fallSpeed-=speedY;
        }

        if(pl.transform.position.x>gameObject.transform.position.x){
            moveSpeed+=speedX;
        }
        else if(pl.transform.position.x<gameObject.transform.position.x){
            moveSpeed-=speedX;
        }

        //離れすぎたら減速
        if(transform.position.y<-6f){
            fallSpeed=0.02f;
            moveSpeed=0;
        }
        if(transform.position.y>6f){
            fallSpeed=-0.02f;
            moveSpeed=0;
        }
        if(transform.position.x<-10f){
            fallSpeed=0;
            moveSpeed=0.02f;
        }
        if(transform.position.x>10f){
            fallSpeed=0;
            moveSpeed=-0.02f;
        }
    }

    void Delete()
    {
        del=true;
        Destroy(gameObject);
    }
}
