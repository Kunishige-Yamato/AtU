using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet26 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    //回転速度
    float rotSpeed;

    GameObject enemy4Prefab;
    GameObject enemy5Prefab;
    GameObject enemy6Prefab;
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;

    void Start()
    {
        enemy4Prefab=GameObject.Find("enemy4Prefab");
        if(enemy4Prefab!=null){
            fallSpeed=(transform.position.y-enemy4Prefab.transform.position.y)/25;
            moveSpeed=(transform.position.x-enemy4Prefab.transform.position.x)/25;
        }
        else{
            enemy5Prefab=GameObject.Find("enemy5Prefab");
            if(enemy5Prefab!=null){
                fallSpeed=(transform.position.y-enemy5Prefab.transform.position.y)/5;
                moveSpeed=(transform.position.x-enemy5Prefab.transform.position.x)/5;
            }
            else{
                enemy6Prefab=GameObject.Find("enemy6Prefab");
                if(enemy6Prefab!=null){
                    fallSpeed=(transform.position.y-enemy6Prefab.transform.position.y)/4;
                    moveSpeed=(transform.position.x-enemy6Prefab.transform.position.x)/4;
                }
                else{
                    Destroy(gameObject);
                }
            }
        }
        rotSpeed=10f;

        int i=Random.Range(0,5);
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
            case 3:
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image3;
                break;
            case 4:
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image4;
                break;
        }
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
