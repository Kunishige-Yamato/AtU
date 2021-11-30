using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    int score;
    float timer,sumTime;

    //進行用
    progress pro;
    int[] modeDif;

    //総合結果用
    public GameObject totalResultCanvas;
    public GameObject sectionPrefab;

    void Start()
    {
        score=0;

        sumTime = 0;

        pro = GameObject.Find("Progress").GetComponent<progress>();
        modeDif = pro.GetDifficulty();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    public void AddScore(int n)
    {
        score+=n;
    }

    public int GetScore()
    {
        return score;
    }

    public float[] GetTime()
    {
        float[] time = { timer, sumTime };
        return time;
    }

    public void ResetScore()
    {
        score=0;
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    public void DoubleScore()
    {
        //score*=(selectDifficulty.difficulty+1);
    }

    public void SetTotalResult(int stageNum, int stageScore, float stageTime)
    {
        //スクロール部分の親になるオブジェ
        GameObject contentParent = totalResultCanvas.transform.Find("StageResults/Viewport/Content").gameObject;
        //sectionPrefab生成 付属オブジェコンポーネント取得
        GameObject sectionObj = Instantiate(sectionPrefab, new Vector3(0, 0, 0), Quaternion.identity, contentParent.transform);
        sectionObj.transform.localScale = new Vector3(1, 1, 1);
        //Name,Score,Timeのテキスト
        Text[] childText = sectionObj.GetComponentsInChildren<Text>();

        //sectionPrefabの中身書き込み
        //ストーリーモード
        if (modeDif[1] == 0)
        {
            childText[0].text = "Stage-" + stageNum;
        }
        else
        {
            childText[0].text = "Section-" + stageNum;
        }
        childText[1].text = "Score:" + stageScore ;
        childText[2].text = "Time :" + stageTime;

        //Name,Score,Timeのテキスト
        GameObject totalTextParent=totalResultCanvas.transform.Find("TotalResults/").gameObject;
        Text[] totalTexts = totalTextParent.GetComponentsInChildren<Text>();
        sumTime += timer;

        //totalresultsの中身書き込み
        totalTexts[1].text = "Score:" + GetScore();
        totalTexts[2].text = "Time :" + (Mathf.Floor(GetTime()[1] * 100) / 100);

        //合計スコア,タイム表示用
        sectionObj.transform.localScale = new Vector3(1, 1, 1);
    }
}
