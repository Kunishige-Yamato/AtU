using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class friend : MonoBehaviour
{
    public InputField UserIdInput;

    public GameObject UserListInfo;
    public GameObject AppPrefab;
    public GameObject ParentContent;

    public GameObject FoundFriend;
    public Image FoundImage;
    public Text FoundID;
    public Text FoundName;
    public Text FoundScore;

    public Text AppID;
    public Text AppName;

    string inputID;
    string returnData;


    // フレンドタブのボタンを押した時
    public void btnFriend()
    {
        // フレンド申請チェック
        StartCoroutine(checkApp(PlayerPrefs.GetString("ID")));
    }
    // フレンド申請チェック
    public IEnumerator checkApp(string id)
    {
        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?chkApp=" + id;
        UnityWebRequest request = UnityWebRequest.Get(url);

        // ヘッダーにJSON形式を指定
        // request.SetRequestHeader("Content-Type", "application/json");

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // phpから受け取った値を&で区切って配列を生成
            string[] getData = request.downloadHandler.text.Split('&');
            ListFormat list_format;
            list_format = UserListInfo.GetComponent<ListFormat>();
            list_format.SetUserList(getData, "app");
            Debug.Log(list_format.app_list[1].name);

            for (int i = 0; i < list_format.app_list.Length; i++)
            {
                GameObject app = Instantiate(AppPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                app.transform.SetParent(ParentContent.transform);
                foreach (Transform child in app.transform)
                {
                    Debug.Log(child.name);
                    if (child.name == "FriendID")
                    {
                        Text f_id = child.GetComponent<Text>();
                        f_id.text = list_format.app_list[i].id;
                    }
                    if (child.name == "FriendName")
                    {
                        Text f_name = child.GetComponent<Text>();
                        f_name.text = list_format.app_list[i].name;
                    }
                }
            }
        }
    }


    // 検索ボタン押した時
    public void btnSearch()
    {
        inputID = UserIdInput.text;

        if (System.Text.RegularExpressions.Regex.IsMatch(inputID, @"^[0-9]{9}$"))
        {
            StartCoroutine(search(inputID));
        }
        else
        {
            Debug.Log("ID値が正しくありません");
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
            if (result == "success")
            {
                Debug.Log("フレンド申請を送信しました");
            }
            else if(result == "false")
            {
                Debug.Log("既にフレンド登録されています");
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
        FoundFriend.SetActive(false);
    }



}