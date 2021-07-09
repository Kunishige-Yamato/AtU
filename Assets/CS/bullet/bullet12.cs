using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet12 : MonoBehaviour
{
    //落下速度
    float fallSpeed=0;
    //横移動速度
    float moveSpeed=0;
    int direction;

    void Start()
    {
        direction=0;
        Invoke("Vertical",Random.Range(0.3f,2f));
    }

    void FixedUpdate()
    {
        if(fallSpeed<=0){
            fallSpeed=0;
        }
        else{
            fallSpeed-=0.005f;
        }
        if(moveSpeed<0&&direction==1){
            moveSpeed=0;
            direction=0;
        }
        else if(moveSpeed>0&&direction==2){
            moveSpeed=0;
            direction=0;
        }
        else if(moveSpeed>0){
            moveSpeed-=0.005f;
        }
        else if(moveSpeed<0){
            moveSpeed+=0.005f;
        }

        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);

        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }

    void Vertical()
    {
        fallSpeed=0.15f;
        Invoke("Side",Random.Range(0.3f,2f));
    }

    void Side()
    {
        float i=Random.Range(0f,1f);
        if(i>0.5f){
            moveSpeed=0.15f;
            direction=1;
        }
        else{
            moveSpeed=-0.15f;
            direction=2;
        }
        Invoke("Vertical",Random.Range(0.3f,2f));
    }
}
