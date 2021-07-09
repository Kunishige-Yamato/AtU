using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet21_1 : MonoBehaviour
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
        if(transform.position.y>0){
            moveSpeed=-0.2f;
        }
        else{
            moveSpeed=0.2f;
        }
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;
        bulletPlace=transform.position;

        if(timer>0.08f){
            Instantiate(bulletPrefab,bulletPlace,Quaternion.identity);
            timer=0;
        }

        //自身の移動
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        //画面外まで行ったら消去
        if(transform.position.x<-10||transform.position.x>10){
            Destroy(gameObject);
        }
    }
}
