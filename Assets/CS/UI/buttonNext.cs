using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonNext : MonoBehaviour
{
    //進行用オブジェ
    GameObject progress;
    progress pro;

    //難易度
    int endless;
    int[] modeDif;

    //ステージ番号
    int[] stage;

    //UI
    public GameObject totalResultCanvas;
    public GameObject resultCanvas;
    public Text nextButtonText;

    void Start()
    {
        //進行用取得
        progress = GameObject.Find("Progress");
        pro = progress.GetComponent<progress>();

        //難易度取得
        modeDif = pro.GetDifficulty();
        endless = modeDif[1];
    }

    void FixedUpdate()
    {
        if(endless==1){
            //エンドレスモードゲームオーバー処理
            if(pro.gameOver)
            {
                Cursor.lockState = CursorLockMode.None;
                nextButtonText.text="Score";
            }

            if (pro.gameOver && gameObject.name == "RetireButton")
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnClick()
    {
        //ステージ番号取得
        stage = pro.GetStageNum();

        Cursor.lockState=CursorLockMode.Locked;
        if(endless==1){
            //エンドレスモードリザルト時のボタン処理
            if(pro.gameOver==true || gameObject.name=="RetireButton")
            {
                //カーソル表示
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //スコア画面へ
                SceneManager.LoadScene("score");
            }
            else{
                StartCoroutine(pro.EndlessStageStart());
            }
        }
        else{
            //ストーリーモードのリザルトのボタン処理
            //全ステージクリアしてたら
            if(stage[0]==stage[1])
            {
                //totlaResult表示してからスコアかタイトルへ
                if(gameObject.name == "TotalResultNextStageButton")
                {
                    //カーソル表示
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //スコア画面へ
                    SceneManager.LoadScene("score");
                }
                else
                {
                    //カーソル表示
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    totalResultCanvas.SetActive(true);
                    resultCanvas.SetActive(false);
                }
            }
            else
            {
                //次の面が最終ステージの時
                if(stage[0] == stage[1] - 1)
                {
                    nextButtonText.text = "Total Result";
                }
                StartCoroutine(pro.StoryStageStart());
            }
        }
    }
}

/*
 明日の俺へ
リザルト頑張って♡
 */
