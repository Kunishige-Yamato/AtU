using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingAdmin : MonoBehaviour
{
    //ランキングPrefab
    public GameObject scorePrefab;

    //スクロール部品の部品入れる親要素
    //無印：世界ランク用　2：フレンドランク用
    public GameObject parentContent;
    public GameObject parentContent2;


    void Start()
    {

    }

    void FixedUpdate()
    {
        
    }

    //個人用プロフィールの生成
    public void PersonalProfile()
    {
        //userTitle
        GameObject userObj = GameObject.Find("UserTitle");
        Text userText = userObj.GetComponent<Text>();
        userText.text = "期待のルーキー";

        //UserName
        userObj = GameObject.Find("UserName");
        userText = userObj.GetComponent<Text>();
        userText.text = "User01";

        //userIcon
        userObj = GameObject.Find("UserIcon");

        //StoryMode
        for(int i=0;i<4;i++)
        {
            string difKind="",modeName="";
            switch(i)
            {
                case 0:
                    difKind = "e";
                    modeName = "Easy Mode : ";
                    break;
                case 1:
                    difKind = "n";
                    modeName = "Normal Mode : ";
                    break;
                case 2:
                    difKind = "h";
                    modeName = "Hard Mode : ";
                    break;
                case 3:
                    difKind = "c";
                    modeName = "Crazy Mode : ";
                    break;

            }
            userObj = GameObject.Find("S_Score_"+difKind);
            userText = userObj.GetComponent<Text>();
            int scoreText = i * 100 + 50;
            userText.text = modeName + scoreText.ToString("D8");
        }

        //EndlessMode
        for (int i = 0; i < 2; i++)
        {
            string difKind = "",modeName = "";
            switch (i)
            {
                case 0:
                    difKind = "n";
                    modeName = "Easy Mode : ";
                    break;
                case 1:
                    difKind = "g";
                    modeName = "Gambling Mode : ";
                    break;

            }
            userObj = GameObject.Find("E_Score_" + difKind);
            userText = userObj.GetComponent<Text>();
            int scoreText = i * 200 +25;
            userText.text = modeName + scoreText.ToString("D8");
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
