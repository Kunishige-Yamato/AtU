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

    public void ShowWorldRanking()
    {
        StartCoroutine(OthersRanking("world"));
    }

    public void ShowFriendRanking()
    {
        StartCoroutine(OthersRanking("friend"));
    }

    //個人用プロフィールの生成
    public IEnumerator PersonalProfile()
    {
        //敵情報をDBから受け取る
        yield return StartCoroutine(jsonRec.ReceivePersonalScoreData(PlayerPrefs.GetString("ID")));
        PersonalScore p_score = jsonRec.GetPersonalScore();

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
    public IEnumerator OthersRanking(string kind)
    {
        //敵情報をDBから受け取る
        yield return StartCoroutine(jsonRec.ReceiveRankingScoreData(PlayerPrefs.GetString("ID"),kind));
        RankingScore[] Ranking = jsonRec.GetRankingScore();

        //親設定
        GameObject rankingParentContent = null;
        if (kind == "world")
        {
            rankingParentContent = parentContent;
        }
        else if (kind == "friend") 
        {
            rankingParentContent = parentContent2;
        }

        //中身一旦削除
        Transform children = rankingParentContent.GetComponentInChildren<Transform>();
        foreach (Transform ob in children)
        {
            Destroy(ob.gameObject);
        }

        //表示人数分ループ
        for (int i=0;i<Ranking.Length;i++)
        {
            GameObject ranking = Instantiate(scorePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ranking.transform.SetParent(rankingParentContent.transform);

            //ここでデータを書き換える

            //UserTitle
            GameObject rankUserTitle = ranking.transform.Find("UserTitle").gameObject;
            Text title = rankUserTitle.GetComponent<Text>();
            title.text = Ranking[i].achieve;

            //UserName
            GameObject rankUserName = ranking.transform.Find("UserName").gameObject;
            Text name = rankUserName.GetComponent<Text>();
            name.text = Ranking[i].name;
            //自分のスコアはテキストに変化を
            if(PlayerPrefs.GetString("ID")==Ranking[i].id)
            {
                name.color = Color.red;
                ranking.transform.Find("Panel").GetComponent<Image>().color = new Color(255, 0, 0, 0.3f);
            }

            //S_Score
            GameObject score = ranking.transform.Find("Score").gameObject;
            Text text = score.GetComponent<Text>();
            text.text = "Hi Score : " + Ranking[i].score.ToString("D8");
        }
    }
}