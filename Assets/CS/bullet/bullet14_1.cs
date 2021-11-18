using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet14_1 : MonoBehaviour
{
    //落下速度
    float fallSpeed=0;
    //横移動速度
    public float moveSpeed=0;
    //bullet14-2呼び出し
    public GameObject bulletPrefab;
    Vector3 bulletPlace;
    float timer;

    void Start()
    {
        if(transform.position.y>=5.5){
            fallSpeed=0.1f;
        }
        else if(transform.position.x<=-9.5f){
            moveSpeed=0.15f;
        }
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;
        bulletPlace=transform.position;

        if(fallSpeed>0&&timer>0.1f){
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
            timer=0;
        }
        if(moveSpeed>0&&timer>0.1f){
            Instantiate(bulletPrefab, bulletPlace, Quaternion.identity).transform.Rotate(new Vector3(0, 0, 90));
            timer =0;
        }

        //自身の移動
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        //画面外まで行ったら消去
        if(transform.position.y<-6||transform.position.x>10){
            Destroy(gameObject);
        }
    }
}
