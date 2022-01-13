using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using scoreInfo;
using System.Text.RegularExpressions;

public class friend : MonoBehaviour
{
    public InputField UserIdInput;

    public GameObject FriCanvas;
    public GameObject FoundFriend;

    public GameObject UserListInfo;
    public GameObject AppPrefab;
    public GameObject ParentContent;

    //フレンドリスト用
    public GameObject friendPrefab;
    public GameObject friendListParent;

    public Image FoundImage;
    public Text FoundID;
    public Text FoundName;
    public Text FoundScore;

    string inputID;
    string returnData;

    //SE関係
    public AudioClip[] audioSEClips;

    // フレンドタブのボタンを押した時
    public void btnFriend()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        FriCanvas.SetActive(true);
        FoundFriend.SetActive(false);

        //フレンドリスト作成
        StartCoroutine(FriendList(PlayerPrefs.GetString("ID")));

        // フレンド申請チェック
        StartCoroutine(checkApp(PlayerPrefs.GetString("ID")));
    }

    //フレンドリスト
    public IEnumerator FriendList(string id)
    {
        string jsonString="";

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?get_friend=" + id;
        UnityWebRequest request = UnityWebRequest.Get(url);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            yield break;
        }
        else
        {
            // phpから受け取った値を&で区切って配列を生成
            jsonString = request.downloadHandler.text;
        }

        //データを分割して配列へ
        Regex regex = new Regex("^.|.$");
        jsonString = regex.Replace(jsonString, "");
        regex = new Regex("},{");
        jsonString = regex.Replace(jsonString, "}&{");

        var jsonDatas = jsonString.Split('&');

        //jsonからオブジェクトに格納
        RankingScore[] r_score = new RankingScore[jsonDatas.Length];
        for (int i = 0; i < jsonDatas.Length; i++)
        {
            r_score[i] = JsonUtility.FromJson<RankingScore>(jsonDatas[i]);
        }

        //中身一旦削除
        Transform children = friendListParent.GetComponentInChildren<Transform>();
        foreach (Transform ob in children)
        {
            Destroy(ob.gameObject);
        }

        //表示人数分ループ
        for (int i = 0; i < r_score.Length; i++)
        {
            GameObject ranking = Instantiate(friendPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ranking.transform.SetParent(friendListParent.transform);

            //ここでデータを書き換える

            //userIcon
            GameObject userIcon = ranking.transform.Find("UserIcon").gameObject;
            Image icon = userIcon.GetComponent<Image>();
            icon.sprite = Resources.Load<Sprite>("AchievementImage/" + Regex.Replace(r_score[i].skin, @"\.png$", ""));

            //UserTitle
            GameObject userTitle = ranking.transform.Find("UserTitle").gameObject;
            Text title = userTitle.GetComponent<Text>();
            title.text = r_score[i].title;

            //UserName
            GameObject userName = ranking.transform.Find("UserName").gameObject;
            Text name = userName.GetComponent<Text>();
            name.text = r_score[i].name;

            //S_Score
            GameObject score = ranking.transform.Find("Score").gameObject;
            Text text = score.GetComponent<Text>();
            text.text = "Hi Score : " + r_score[i].score.ToString("D8");
        }
    }

    // フレンド申請チェック
    public IEnumerator checkApp(string id)
    {
        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?chkApp=" + id;
        UnityWebRequest request = UnityWebRequest.Get(url);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // フレンド申請一覧のプレハブを消す
            GameObject content = FriCanvas.transform.Find("Application_Approval/Scroll View/Viewport/Content").gameObject;
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }

            string res = request.downloadHandler.text;
            if (res == "failure")
            {
                Debug.Log("取得件数 = ０件");
            }
            else
            {
                // phpから受け取った値を&で区切って配列を生成
                string[] getData = res.Split('&');
                ListFormat list_format;
                list_format = UserListInfo.GetComponent<ListFormat>();
                list_format.SetUserList(getData, "app");

                for (int i = 0; i < list_format.app_list.Length; i++)
                {
                    GameObject app = Instantiate(AppPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    app.transform.SetParent(ParentContent.transform);
                    foreach (Transform child in app.transform)
                    {

                        Text f_id;
                        Text f_name;

                        //Debug.Log(child.name);
                    
                        if (child.name == "FriendID")
                        {
                            f_id = child.GetComponent<Text>();
                            f_id.text = list_format.app_list[i].id;
                        }
                        if (child.name == "FriendName")
                        {
                            f_name = child.GetComponent<Text>();
                            f_name.text = list_format.app_list[i].name;
                        }
                    }
                }
            }
        }
    }

    // 検索ボタン押した時
    public void btnSearch()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        inputID = UserIdInput.text;

        if(inputID == PlayerPrefs.GetString("ID") || !System.Text.RegularExpressions.Regex.IsMatch(inputID, @"^[0-9]{9}$"))
        {
            Debug.Log("このIDは検索できません");
        }
        else
        {
            StartCoroutine(search(inputID));
        }
    }

    // 検索メソッド
    public IEnumerator search(string id)
    {
        string[] searchData;

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?search=" + id;
        UnityWebRequest request = UnityWebRequest.Get(url);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            returnData = request.downloadHandler.text;
            if (returnData == "empty")
            {
                // Debug.Log("検索結果が空でした");
                FoundID.text = "---------";
                FoundName.text = "検索結果が空でした";
                FoundScore.text = "---------";
                FoundFriend.SetActive(true);
            }
            else
            {
                searchData = returnData.Split(',');
                FoundID.text = searchData[0];
                FoundName.text = searchData[1];
                FoundScore.text = searchData[2];
                FoundFriend.SetActive(true);
            }
        }
    }

    // OKボタンを押した時
    public void btnOK()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        Debug.Log(FoundID.text);
        StartCoroutine(request(PlayerPrefs.GetString("ID"), FoundID.text));
    }
    // フレンド申請メソッド
    public IEnumerator request(string id1, string id2)
    {
        string result;

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?user1="+id1+"&user2="+id2;
        UnityWebRequest request = UnityWebRequest.Get(url);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            result = request.downloadHandler.text;
            if (result == "already")
            {
                Debug.Log("既にフレンド登録されています");
            }
            else if (result == "requested")
            {
                Debug.Log("フレンド申請済みのユーザーです");
            }
            else if (result == "success")
            {
                Debug.Log("フレンド申請を送信しました");
                FoundFriend.SetActive(false);
            }
            else if(result == "failure")
            {
                Debug.Log("登録に失敗しました");
            }
            else
            {
                Debug.Log(result);
            }
        }
    }

    // キャンセルボタン押した時
    public void btnCancel()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        FoundFriend.SetActive(false);
    }
}