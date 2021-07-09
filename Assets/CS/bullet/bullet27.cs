using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet27 : MonoBehaviour
{
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;

    GameObject enemyPrefab;
    public Sprite image0;
    public Sprite image1;

    void Start()
    {
        enemyPrefab=GameObject.Find("enemy8Prefab");
        fallSpeed=(transform.position.y-enemyPrefab.transform.position.y)/20;
        moveSpeed=(transform.position.x-enemyPrefab.transform.position.x)/20;

        //enemyと自分自身の距離を測る
        Vector3 enemyPos=enemyPrefab.transform.position;
        Vector3 bulletPos=transform.position;
        float dis=Vector3.Distance(enemyPos,bulletPos);
        if(dis>1f){
            this.gameObject.GetComponent<SpriteRenderer>().sprite=image0;
        }
        else{
            this.gameObject.GetComponent<SpriteRenderer>().sprite=image1;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(moveSpeed,fallSpeed,0,Space.World);

        //端まで行ったら消去
        if(transform.position.y<-5.5f||transform.position.y>5.5f||transform.position.x<-9.5f||transform.position.x>9.5f){
            Destroy(gameObject);
        }
    }
}
