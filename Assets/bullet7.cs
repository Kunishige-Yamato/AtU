using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet7 : MonoBehaviour
{
    //移動速度
    float moveSpeed;
    //中身へ飛ぶ用
    int direction;

    void Start()
    {
        this.moveSpeed=0f;
        if(transform.position.x>=0){
            direction=-1;
        }
        else{
            direction=1;
        }
    }

    void FixedUpdate()
    {
        moveSpeed+=Mathf.Abs(0.003f)*direction;
        transform.Translate(moveSpeed,0,0,Space.World);
        //左右まで行ったら消去
        if(transform.position.x>10||transform.position.x<-10){
            Destroy(gameObject);
        }
    }
}
