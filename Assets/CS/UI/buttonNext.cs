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

        //ストーリーモードはリタイアボタンなし
        if (endless == 0 && gameObject.name == "ExitButton") 
        {
            Destroy(gameObject);
        }
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

            //ゲームオーバーになったらリトライ不能
            if (pro.gameOver && gameObject.name == "ExitButton")
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
            //エンドレスモード｜途中退席
            if(pro.gameOver==true || gameObject.name=="ExitButton")
            {
                //カーソル表示
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                //点数を保存させる処理
                pro.SaveScore();

                //スコア画面へ
                SceneManager.LoadScene("score");
            }
            else
            {
                StartCoroutine(pro.EndlessStageStart());
            }
        }
        else{
            //ストーリーモードのリザルトのボタン処理
            //全ステージクリアしてたら
            if (stage[0]==stage[1])
            {
                //totlaResult表示してからスコアかタイトルへ
                if(gameObject.name == "TotalResultNextStageButton")
                {
                    //カーソル表示
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    //スコア保存
                    pro.SaveScore();

                    //スコア画面へ
                    SceneManager.LoadScene("score");
                }
                else
                {
                    //カーソル表示
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    //リザルト切り替えて表示
                    CanvasGroup cg=totalResultCanvas.GetComponent<CanvasGroup>();
                    cg.alpha = 1;
                    cg.interactable = true;
                    cg.blocksRaycasts = true;
                    cg=resultCanvas.GetComponent<CanvasGroup>();
                    cg.alpha = 0;
                    cg.interactable = false;
                    cg.blocksRaycasts = false;
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
