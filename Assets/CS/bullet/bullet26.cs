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
    public Sprite[] image=new Sprite[4];
    public float timer=0;

    public int parent=0;

    void Start()
    {
        if(enemyPrefab!=null)
        {
            switch (parent)
            {
                case 4:
                    fallSpeed = (transform.position.y - enemyPrefab.transform.position.y) / 25;
                    moveSpeed = (transform.position.x - enemyPrefab.transform.position.x) / 25;
                    Invoke("checkParent", 0.05f);
                    break;
                case 5:
                    fallSpeed = (transform.position.y - enemyPrefab.transform.position.y) / 5;
                    moveSpeed = (transform.position.x - enemyPrefab.transform.position.x) / 5;
                    break;
                case 6:
                    fallSpeed = (transform.position.y - enemyPrefab.transform.position.y) / 4;
                    moveSpeed = (transform.position.x - enemyPrefab.transform.position.x) / 4;
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }

            rotSpeed = 10f;

            progress pro = GameObject.Find("Progress").GetComponent<progress>();
            int ran = Random.Range(0, 4);
            int num;
            if(pro.GetStageNum()[0]==4)
            {
                num = 2;
            }
            else
            {
                num = 0;
            }
            switch (ran)
            {
                case 0:
                case 1:
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = image[0+num];
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = image[1+num];
                    break;
            }
        }

        //親がやられたら消去
        if(enemyPrefab==null&&fallSpeed==0&&moveSpeed==0){
            Destroy(gameObject);
        }
    }

    void LateUpdate()
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
