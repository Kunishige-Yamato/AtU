using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet3 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    public float moveSpeed=0;
    //回転速度
    float rotSpeed;
    GameObject pl;

    void Start()
    {
        this.fallSpeed=0.05f;
        this.moveSpeed=0;
        this.rotSpeed=10;

        pl=GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if(pl.transform.position.x>gameObject.transform.position.x){
            moveSpeed+=0.001f;
        }
        else if(pl.transform.position.x<gameObject.transform.position.x){
            moveSpeed-=0.001f;
        }
        else{
            moveSpeed=0;
        }
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);

        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }

    }
}
