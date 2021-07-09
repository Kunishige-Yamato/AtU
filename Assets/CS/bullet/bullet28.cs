using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet28 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;
    //回転速度
    float rotSpeed;
    float timer=0;

    GameObject player;
    public Sprite image0;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;
    public Sprite image6;

    void Start()
    {
        player=GameObject.Find("Player");
        
        fallSpeed=(transform.position.y-player.transform.position.y)/-20;
        moveSpeed=(transform.position.x-player.transform.position.x)/-20;

        int i=Random.Range(0,7);
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
            case 5:
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image5;
                break;
            case 6:
                this.gameObject.GetComponent<SpriteRenderer>().sprite=image6;
                break;
        }

        rotSpeed=10f;
        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;
        if(timer>2){
            transform.Translate(moveSpeed,fallSpeed,0,Space.World);
        }

        transform.Rotate(0,0,rotSpeed);

        //端まで行ったら消去
        if(transform.position.y<-7f||transform.position.y>7f||transform.position.x<-11f||transform.position.x>11f){
            Destroy(gameObject);
        }
    }
}
