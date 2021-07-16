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

    public GameObject enemyPrefab;
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public float timer=0;

    public int parent=0;

    void Start()
    {
        switch(parent){
            case 4:
                fallSpeed=(transform.position.y-enemyPrefab.transform.position.y)/25;
                moveSpeed=(transform.position.x-enemyPrefab.transform.position.x)/25;
                Invoke("checkParent",0.05f);
                break;
            case 5:
                fallSpeed=(transform.position.y-enemyPrefab.transform.position.y)/5;
                moveSpeed=(transform.position.x-enemyPrefab.transform.position.x)/5;
                break;
            case 6:
                fallSpeed=(transform.position.y-enemyPrefab.transform.position.y)/4;
                moveSpeed=(transform.position.x-enemyPrefab.transform.position.x)/4;
                break;
            default:
                Destroy(gameObject);
                break;
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

        //親がやられたら消去
        if(enemyPrefab==null&&fallSpeed==0&&moveSpeed==0){
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        transform.Translate(moveSpeed,fallSpeed,0,Space.World);
        transform.Rotate(0,0,rotSpeed);

        //端まで行ったら消去
        if(transform.position.y<-5.5f||transform.position.y>9f||transform.position.x<-9.5f||transform.position.x>9.5f){
            Destroy(gameObject);
        }
        //親がやられたら消去
        if(enemyPrefab==null&&fallSpeed==0&&moveSpeed==0){
            Destroy(gameObject);
        }
    }

    void checkParent()
    {
        if(enemyPrefab==null&&timer>0.1f){
            Destroy(gameObject);
        }
    }
}
