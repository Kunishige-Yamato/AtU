using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using scoreInfo;

public class RankingAdmin : MonoBehaviour
{
    //ランキングPrefab
    public GameObject scorePrefab;

    //スクロール部品の部品入れる親要素
    //無印：世界ランク用　2：フレンドランク用
    public GameObject parentContent;
    public GameObject parentContent2;

    //DB接続用
    jsonReceive jsonRec = new jsonReceive();

    void Start()
    {
        StartCoroutine(PersonalProfile());
    }

    public void ShowPersonalProfile()
    {
        StartCoroutine(PersonalProfile());
    }

    //個人用プロフィールの生成
    public IEnumerator PersonalProfile()
    {
        //敵情報をDBから受け取る
        yield return StartCoroutine(jsonRec.ConnectDB(PlayerPrefs.GetString("ID")));
        PersonalScore p_score=jsonRec.GetPersonalScore();

        //userTitle
        GameObject userObj = GameObject.Find("UserTitle");
        Text userText = userObj.GetComponent<Text>();
        userText.text = p_score.achieve;

        //UserName
        userObj = GameObject.Find("UserName");
        userText = userObj.GetComponent<Text>();
        userText.text = p_score.name;

        //userIcon
        userObj = GameObject.Find("UserIcon");

        //StoryMode
        for(int i=0;i<4;i++)
        {
            switch(i)
            {
                case 0:
                    userObj = GameObject.Find("S_Score_e");
                    userText = userObj.GetComponent<Text>();
                    userText.text = "Easy Mode  " + p_score.s_easy.ToString("D8");
                    break;
                case 1:
                    userObj = GameObject.Find("S_Score_n");
                    userText = userObj.GetComponent<Text>();
                    userText.text = "Normal Mode  " + p_score.s_normal.ToString("D8");
                    break;
                case 2:
                    userObj = GameObject.Find("S_Score_h");
                    userText = userObj.GetComponent<Text>();
                    userText.text = "Hard Mode  " + p_score.s_hard.ToString("D8");
                    break;
                case 3:
                    userObj = GameObject.Find("S_Score_c");
                    userText = userObj.GetComponent<Text>();
                    userText.text = "Crazy Mode  " + p_score.s_crazy.ToString("D8");
                    break;

            }
        }

        //EndlessMode
        for (int i = 0; i < 2; i++)
        {
            switch (i)
            {
                case 0:
                    userObj = GameObject.Find("E_Score_n");
                    userText = userObj.GetComponent<Text>();
                    userText.text = "Normal Mode  " + p_score.e_normal.ToString("D8");
                    break;
                case 1:
                    userObj = GameObject.Find("E_Score_g");
                    userText = userObj.GetComponent<Text>();
                    userText.text = "Gambling Mode  " + p_score.e_gambling.ToString("D8");
                    break;

            }
        }
    }

    //ワールドランキングの中身生成
    public void AllWorldRanking()
    {
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
            GameObject score = ranking.transform.Find("Score").gameObject;
            Text text = score.GetComponent<Text>();
            //スコア代入的な
            int j = i * 50;
            text.text = "Hi Score : " + j.ToString("D8");
        }
    }

    //フレンドランキングの中身生成
    public void FriendRanking()
    {
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
            GameObject score = ranking.transform.Find("Score").gameObject;
            Text text = score.GetComponent<Text>();
            //スコア代入的な
            int j = i * 50;
            text.text = "Hi Score : " + j.ToString("D8");
        }
    }
}