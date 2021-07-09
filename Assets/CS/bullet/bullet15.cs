using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet15 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    public float moveSpeed=0;
    //回転速度
    float rotSpeed;
    //重力
    float gra=-0.00015f;
    //イラスト
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;

    void Start()
    {
        int i=Random.Range(0,3);
        switch(i){
            case 0:
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image0;
                break;
            case 1:
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image1;
                break;
            case 2:
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image2;
                break;
        }
        fallSpeed=-0.05f;
        moveSpeed=Random.Range(-0.02f,0.02f);
        rotSpeed=Random.Range(-5f,5f);
    }

    void FixedUpdate()
    {
        fallSpeed-=gra;
        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);
        //下まで行ったら消去
        if(transform.position.y<-5.5f){
            Destroy(gameObject);
        }
    }
}
