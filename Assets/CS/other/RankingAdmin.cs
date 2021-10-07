using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingAdmin : MonoBehaviour
{
    //ランキングPrefab
    public GameObject scorePrefab;
    //無印：世界ランク用　2：フレンドランク用
    public GameObject parentContent;
    public GameObject parentContent2;

    void Start()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void AllWorldRanking()
    {
        //ワールドランキングの中身生成
        for(int i=0;i<10;i++)
        {
            GameObject ranking=Instantiate(scorePrefab,new Vector3(0,0,0),Quaternion.identity)as GameObject;
            ranking.transform.SetParent(parentContent.transform);

            //ここでデータを書き換える

            //UserTitle
            GameObject rankUserTitle = ranking.transform.Find("UserTitle").gameObject;
            Text title = rankUserTitle.GetComponent<Text>();
            title.text = "期待のルーキー";

            //UserName
            GameObject rankUserName = ranking.transform.Find("UserName").gameObject;
            Text name = rankUserName.GetComponent<Text>();
            name.text = "user-" + i;

            //S_Score
            GameObject s_score = ranking.transform.Find("S_Score").gameObject;
            Text s_text = s_score.GetComponent<Text>();
            //スコア代入的な
            int j = i * 50;
            s_text.text = "Story Mode Hi Score : " + j;

            //E_Score
            GameObject e_score = ranking.transform.Find("E_Score").gameObject;
            Text e_text = e_score.GetComponent<Text>();
            //スコア代入的な
            int k = i * 100;
            e_text.text = "Endless Mode Hi Score : " + k;
        }
    }

    public void FriendRanking()
    {
        //フレンドランキングの中身生成
        for (int i = 0; i < 10; i++)
        {
            GameObject ranking = Instantiate(scorePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            ranking.transform.SetParent(parentContent2.transform);

            //ここでデータを書き換える

            //UserTitle
            GameObject rankUserTitle = ranking.transform.Find("UserTitle").gameObject;
            Text title = rankUserTitle.GetComponent<Text>();
            title.text = "期待のルーキー";

            //UserName
            GameObject rankUserName = ranking.transform.Find("UserName").gameObject;
            Text name = rankUserName.GetComponent<Text>();
            name.text = "user-" + i;

            //S_Score
            GameObject s_score = ranking.transform.Find("S_Score").gameObject;
            Text s_text = s_score.GetComponent<Text>();
            //スコア代入的な
            int j = i * 50;
            s_text.text = "Story Mode Hi Score : " + j;

            //E_Score
            GameObject e_score = ranking.transform.Find("E_Score").gameObject;
            Text e_text = e_score.GetComponent<Text>();
            //スコア代入的な
            int k = i * 100;
            e_text.text = "Endless Mode Hi Score : " + k;
        }
    }
}
