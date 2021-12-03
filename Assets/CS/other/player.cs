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
    Slider hpBar;

    int difficulty,endless;

    CanvasGroup resultGroup;
    public CanvasGroup pauseGroup;

    //進行用
    GameObject progress;
    progress pro;

    // 位置座標
	private Vector3 mousePosition;
	// スクリーン座標をワールド座標に変換した位置座標
	public Vector3 screenToWorldPointPosition;

    CursorLockMode wantedMode = CursorLockMode.None;

    //爆発エフェクトのPrefab
	public GameObject explosionPrefab;

    //隠しコマンド入力待ちフラグ
    int commandNum = 0;
    public Sprite commandImg;

    void Start()
    {
        pl=GameObject.Find("Player");
        bulPos.x=gameObject.transform.position.x;
        bulPos.y=gameObject.transform.position.y+0.6f;
        bulPos.z=gameObject.transform.position.z;
        coolTime=0.1f;
        timer=coolTime;
        imageNum=0;

        //進行用コンポーネント
        progress = GameObject.Find("Progress");
        pro = progress.GetComponent<progress>();

        //難易度とモードを取得
        difficulty = pro.GetDifficulty()[0];
        endless = pro.GetDifficulty()[1];

        if (endless == 1)
        {
            //hpバー制御
            hpBar = GameObject.Find("EndlessHP/Slider-Player").GetComponent<Slider>();
            hpBar.maxValue = maxLife;
            hpBar.value = hpBar.maxValue;
        }

        //リザルト消去
        if (endless == 0)
        {
            resultGroup = GameObject.Find("PauseCanvas/Result").GetComponent<CanvasGroup>();
        }
        else
        {
            resultGroup = GameObject.Find("PauseCanvas/TotalResult").GetComponent<CanvasGroup>();
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

    void Update()
    {
        //隠しコマンド
        if (Input.GetKeyDown(KeyCode.UpArrow) && commandNum == 0)
        {
            commandNum = 1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && commandNum == 1)
        {
            commandNum = 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && commandNum == 2)
        {
            commandNum = 3;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && commandNum == 3)
        {
            commandNum = 4;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && commandNum == 4)
        {
            commandNum = 5;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && commandNum == 5)
        {
            commandNum = 6;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && commandNum == 6)
        {
            commandNum = 7;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && commandNum == 7)
        {
            commandNum = 8;
        }
        else if (Input.GetKeyDown(KeyCode.B) && commandNum == 8)
        {
            commandNum = 9;
        }
        else if (Input.GetKeyDown(KeyCode.A) && commandNum == 9)
        {
            //トロフィーの達成
            Debug.Log("command success!");

            /*
            //当たり判定無くして無敵
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            */

            SpriteRenderer image = gameObject.GetComponent<SpriteRenderer>();
            image.sprite = commandImg;

            //当たり判定縮小する強化
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            collider.radius = 0.1f;
        }
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        //エンドレスモードの時hpバー表示
        if(endless==1)
        {
            hpBar.value=maxLife-hitNum;
        }

        bulPos.x=gameObject.transform.position.x;
        bulPos.y=gameObject.transform.position.y+0.6f;
        bulPos.z=gameObject.transform.position.z;

        if (Input.GetKey(KeyCode.Escape) && resultGroup.alpha != 1f) 
        {
            //自機停止
            canMove=false;
            Cursor.visible=true;

            //ポーズ画面表示
            pauseGroup.alpha=1f;
            pauseGroup.interactable = true;
            pauseGroup.blocksRaycasts = true;
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
        if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss") && pro.gameOver == false) 
        {
            // 爆発エフェクトを生成する	
		    Instantiate (explosionPrefab, transform.position, Quaternion.identity);

            //スコア大幅減点
            GameObject scoreCounter=GameObject.Find("ScoreCounter");
            ScoreCount sc=scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(-100 * (difficulty + 1));
		
            hitNum++;

            if(hitNum>=maxLife){
                if (endless==1)
                {
                    pro.gameOver=true;
                    pro.DisplayResult();
                    Destroy(gameObject);
                }
            }
        }
    }
}
