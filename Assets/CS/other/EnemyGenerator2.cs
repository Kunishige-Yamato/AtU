using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EnemyGenerator2 : MonoBehaviour
{
    public GameObject boss1Prefab;
    public GameObject boss2Prefab;
    public GameObject boss3Prefab;
    public GameObject boss4_1Prefab;
    public GameObject boss4_2Prefab;

    /*
    //カットイン関係
    public GameObject cutInCanvas;
    Animator animator;
    public Sprite cutImage1;
    public Sprite cutImage2;
    public Sprite cutImage3;
    public Sprite cutImage4;
    */

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas=new List<string[]>(); // CSVの中身を入れるリスト;

    public int stageNum=0;
    public int allStageNum=0;
    public float timer,sumTime;

    GameObject player;
    player pl;

    public CanvasGroup resultGroup;
    public CanvasGroup pauseGroup;
    public CanvasGroup hpBarGroup;
    public Text stageText;
    public Text scoreText;
    public Text timeText;
    public Text hitText;
    int stageScore=0,stageHit=0;

    // 位置座標
	private Vector3 mousePosition;
	// スクリーン座標をワールド座標に変換した位置座標
	private Vector3 screenToWorldPointPosition;

    CursorLockMode wantedMode = CursorLockMode.None;

    int ran;
    //ボス選択配列初期化
    int[] selectBoss=Enumerable.Repeat<int>(0,4).ToArray();

    void Start()
    {
        player=GameObject.Find("Player");
        pl=player.GetComponent<player>();

        sumTime=0;

        //テスト用ステージスキップ
        //stageNum=3;

        // 初期動作
        Cursor.lockState=wantedMode;
        Cursor.lockState=wantedMode=CursorLockMode.Confined;
        Cursor.visible=false; 

        //animator=cutInCanvas.GetComponent<Animator>();

        ReadFile();
    }

    public void DisplayResult()
    {
        //自機停止
        pl.canMove=false;

        //カーソル表示
        Cursor.visible=true; 

        //hpバー消去
        hpBarGroup.alpha=0f;
        
        //敵消去
        //タグつきを全て格納
        GameObject[] enemys=GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys) {
            Destroy(del);
        }

        //リザルト表示
        resultGroup.alpha=1f;
        resultGroup.interactable=true;

        //スコア集計＆表示
        stageText.text="Section-"+stageNum;
        GameObject scoreCounter=GameObject.Find("ScoreCounter");
        ScoreCount sc=scoreCounter.GetComponent<ScoreCount>();
        scoreText.text="Score:"+(sc.returnScore()-stageScore);
        stageScore=sc.returnScore();
        timeText.text="Time:"+(Mathf.Floor(timer*100)/100);
        sumTime+=timer;
        hitText.text="Hit:"+(pl.hitNum-stageHit);
        stageHit=pl.hitNum;
    }

    /*
    void CutIn()
    {
        switch(stageNum){
            case 1:
                cutInCanvas.GetComponent<Image>().sprite=cutImage1;
                break;
            case 2:
                cutInCanvas.GetComponent<Image>().sprite=cutImage2;
                break;
            case 3:
                cutInCanvas.GetComponent<Image>().sprite=cutImage3;
                break;
            case 4:
                cutInCanvas.GetComponent<Image>().sprite=cutImage4;
                break;
        }

        animator.Play("CutIn",0,0f);
    }
    */

    public void ReadFile()
    {
        //自機動作
        Cursor.visible=false;
        pl.canMove=true;
        Cursor.lockState=CursorLockMode.Locked;
        Invoke("CanMove",0.1f);

        //hpバー消去
        hpBarGroup.alpha=0f;

        //リザルト消去
        resultGroup.alpha=0f;
        resultGroup.interactable=false;

        //ポーズ画面消去
        pauseGroup.alpha=0f;
        pauseGroup.interactable=false;

        stageNum++;

        //CutIn();

        //boss呼び出し
        for(int i=0;i<selectBoss.Length;i++){
            if(selectBoss[i]==0){
                ran=Random.Range(1,5);
                if(i==0){
                    selectBoss[i]=ran;
                    break;
                }
                while(selectBoss[0]==ran||selectBoss[1]==ran||selectBoss[2]==ran){
                    ran=Random.Range(1,5);
                }
                selectBoss[i]=ran;
                break;
            }
        }
        if(selectBoss[3]!=0){
            selectBoss=Enumerable.Repeat<int>(0,4).ToArray();
        }

        Generate(ran);

        if(stageNum!=1&&stageNum%4==1&&selectDifficulty.difficulty<3){
            selectDifficulty.difficulty++;
        }

        timer=0;
    }

    void FixedUpdate()
    {
        timer+=Time.deltaTime;
    }

    void CanMove()
    {
        Cursor.lockState=wantedMode=CursorLockMode.Confined;
    }

    void Generate(int n)
    {   
        switch(n){
            case 1:
                Instantiate(boss1Prefab,new Vector3(0,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
            case 2:
                Instantiate(boss2Prefab,new Vector3(0,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
            case 3:
                Instantiate(boss3Prefab,new Vector3(0,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
            case 4:
                Instantiate(boss4_1Prefab,new Vector3(-5,3,0),Quaternion.identity);
                Instantiate(boss4_2Prefab,new Vector3(5,3,0),Quaternion.identity);
                hpBarGroup.alpha=1f;
                break;
        }
    }
}

/*
    bossに使われてるegの名前直しから
*/
