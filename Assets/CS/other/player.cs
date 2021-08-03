using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    GameObject pl;
	public GameObject bulletPrefab;
    Vector3 bulPos;
    float timer,coolTime;
    public int imageNum;
    public int hitNum;
    public bool canMove=true;
    int maxLife=30;
    public bool gameOver=false;
    GameObject eg;
    EnemyGenerator2 enemyGenerator2;
    Slider hpBar;

    public CanvasGroup resultGroup;
    public CanvasGroup pauseGroup;

    // 位置座標
	private Vector3 mousePosition;
	// スクリーン座標をワールド座標に変換した位置座標
	public Vector3 screenToWorldPointPosition;

    CursorLockMode wantedMode = CursorLockMode.None;

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

        if(selectDifficulty.endless){
            eg=GameObject.Find("EG");
            enemyGenerator2=eg.GetComponent<EnemyGenerator2>();
            //hpバー制御
            hpBar=GameObject.Find("Slider-Player").GetComponent<Slider>();
            hpBar.maxValue=maxLife;
            hpBar.value=hpBar.maxValue;
        }

        // 初期動作
        Cursor.lockState=wantedMode;
        Cursor.lockState=wantedMode=CursorLockMode.Confined;
        Cursor.visible=false; 
        Cursor.lockState=CursorLockMode.Locked;
        Invoke("CursorMove",0.1f);
    }

    void CursorMove()
    {
        Cursor.lockState=CursorLockMode.None;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        if(selectDifficulty.endless){
            hpBar.value=maxLife-hitNum;
        }

        bulPos.x=gameObject.transform.position.x;
        bulPos.y=gameObject.transform.position.y+0.6f;
        bulPos.z=gameObject.transform.position.z;

        if (Input.GetKey (KeyCode.Escape)&&resultGroup.alpha!=1f)
        {
            //自機停止
            canMove=false;
            Cursor.visible=true;

            //ポーズ画面表示
            pauseGroup.alpha=1f;
            pauseGroup.interactable=true;
        }

        /*
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
        */

        if(canMove)
        {
            // Vector3でマウス位置座標を取得する
            mousePosition=Input.mousePosition;
            // Z軸修正
            mousePosition.z=10f;
            // マウス位置座標をスクリーン座標からワールド座標に変換する
            screenToWorldPointPosition=Camera.main.ScreenToWorldPoint(mousePosition);

            if(screenToWorldPointPosition.x<8.55f&&screenToWorldPointPosition.x>-8.55f&&screenToWorldPointPosition.y>-4.61f&&screenToWorldPointPosition.y<4.61f){
                // ワールド座標に変換されたマウス座標を代入
                gameObject.transform.position=screenToWorldPointPosition;
            }
            //下にはみ出した時
            if(screenToWorldPointPosition.y<=-4.61f){
                //Y軸固定
                screenToWorldPointPosition.y=-4.61f;
                // ワールド座標に変換されたマウス座標を代入
                gameObject.transform.position=screenToWorldPointPosition;
            }
            //上にはみ出した時
            if(screenToWorldPointPosition.y>=4.61f){
                //Y軸固定
                screenToWorldPointPosition.y=4.61f;
                // ワールド座標に変換されたマウス座標を代入
                gameObject.transform.position=screenToWorldPointPosition;
            }
            //左にはみ出した時
            if(screenToWorldPointPosition.x<=-8.55f){
                //X軸固定
                screenToWorldPointPosition.x=-8.55f;
                // ワールド座標に変換されたマウス座標を代入
                gameObject.transform.position=screenToWorldPointPosition;
            }
            //右にはみ出した時
            if(screenToWorldPointPosition.x>=8.55f){
                //X軸固定
                screenToWorldPointPosition.x=8.55f;
                // ワールド座標に変換されたマウス座標を代入
                gameObject.transform.position=screenToWorldPointPosition;
            }

            //スペースキー
            if(Input.GetKey(KeyCode.Space)&&timer>=coolTime) {
                Instantiate(bulletPrefab,bulPos,Quaternion.identity);
                imageNum++;
                imageNum%=2;
                timer=0;
            }
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

            if(hitNum>=maxLife){
                gameOver=true;
                enemyGenerator2.DisplayResult();
            }
        }
    }
}
