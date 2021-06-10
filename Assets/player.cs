using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    GameObject pl;
	public GameObject bulletPrefab;
    Vector3 bulPos;
    float timer,coolTime;
    public int imageNum;
    public int hitNum;

    //爆発エフェクトのPrefab
	public GameObject explosionPrefab;

    void Start()
    {
        pl=GameObject.Find("Player");
        bulPos.x=gameObject.transform.position.x;
        bulPos.y=gameObject.transform.position.y+0.6f;
        bulPos.z=gameObject.transform.position.z;
        coolTime=0.1f;
        timer=coolTime;
        imageNum=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        bulPos.x=gameObject.transform.position.x;
        bulPos.y=gameObject.transform.position.y+0.6f;
        bulPos.z=gameObject.transform.position.z;

        //上
        if (Input.GetKey (KeyCode.W)) {
            if(pl.transform.position.y<4.61f){
			    transform.Translate (0,0.2f,0);
            }
		}
        //下
		if (Input.GetKey (KeyCode.S)) {
            if(pl.transform.position.y>-4.6f){
			    transform.Translate (0,-0.2f,0);
            }
		}
        //左
		if (Input.GetKey (KeyCode.A)) {
            if(pl.transform.position.x>-8.55f){
			    transform.Translate (-0.2f,0,0);
            }
		}
        //右
		if (Input.GetKey (KeyCode.D)) {
            if(pl.transform.position.x<8.55f){
			    transform.Translate (0.2f,0,0);
            }
		}
        //スペースキー
		if(Input.GetKey(KeyCode.Space)&&timer>=coolTime) {
            Instantiate(bulletPrefab,bulPos,Quaternion.identity);
            imageNum++;
            imageNum%=2;
            timer=0;
		}
    }

    //当たったら死
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Enemy")
        {
            // 爆発エフェクトを生成する	
		    Instantiate (explosionPrefab, transform.position, Quaternion.identity);

            //スコア大幅減点
            GameObject scoreCounter=GameObject.Find("ScoreCounter");
            ScoreCount sc=scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(-100);
		
            hitNum++;
        }
    }
}
