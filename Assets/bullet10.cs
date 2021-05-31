using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet10 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    public float moveSpeed=0;

    void Start()
    {
        this.fallSpeed=0.1f;
        this.moveSpeed=0;
    }

    void FixedUpdate()
    {
        if(gameObject.transform.position.y<-1){
            fallSpeed=0.02f;
        }
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }
}
