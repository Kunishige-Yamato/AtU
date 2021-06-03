using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet22 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    //回転速度
    float rotSpeed;

    GameObject mainCake;
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;

    void Start()
    {
        if(transform.position.x>0){
            mainCake=GameObject.Find("boss4-2Prefab(Clone)");
            int i=Random.Range(0,2);
            if(i>0){
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image0;
            }
            else{
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image1;
            }
        }
        else{
            mainCake=GameObject.Find("boss4-1Prefab(Clone)");
            int i=Random.Range(0,2);
            if(i>0){
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image2;
            }
            else{
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image3;
            }
        }
        fallSpeed=(transform.position.y-mainCake.transform.position.y)/25;
        moveSpeed=(transform.position.x-mainCake.transform.position.x)/25;
        rotSpeed=8f;
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);

        //端まで行ったら消去
        if(transform.position.y<-5.5f||transform.position.y>5.5f||transform.position.x<-9.5f||transform.position.x>9.5f){
            Destroy(gameObject);
        }
    }
}
