using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonMoveScene : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void Onclick()
    {
        switch (gameObject.name)
        {
            case "StartBtn":
                SceneManager.LoadScene("selectMode");
                break;
            case "ScoreBtn":
                SceneManager.LoadScene("score");
                break;
            case "AchievementBtn":
                SceneManager.LoadScene("achievement");
                break;
            default:
                if (SceneManager.GetActiveScene().name == "game_1" && gameObject.name != "PauseBackTitleButton")
                {
                    progress pro = GameObject.Find("Progress").GetComponent<progress>();

                    StartCoroutine(SaveData(pro));
                }
                break;
        }
    }

    public IEnumerator SaveData(progress pro)
    {
        //点数を保存させる処理
        yield return StartCoroutine(pro.SaveScore());
        yield return StartCoroutine(pro.SaveUserData());

        //スコア画面へ
        SceneManager.LoadScene("title");
    }
}
