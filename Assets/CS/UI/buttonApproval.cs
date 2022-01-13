using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class buttonApproval : MonoBehaviour
{
    public GameObject btnFriend;

    //SE関係
    public AudioClip[] audioSEClips;

    // フレンド申請拒否ボタン
    public void BtnDeny()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        GameObject parent = transform.parent.gameObject;
        Text id = parent.transform.Find("FriendID").gameObject.GetComponent<Text>();
        StartCoroutine(Deny(id.text, PlayerPrefs.GetString("ID")));
    }
    // フレンド申請拒否処理
    public IEnumerator Deny(string id1, string id2)
    {
        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?application=deny&id1="+id1+"&id2="+id2;
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
            string res = request.downloadHandler.text;
            if (res == "success")
            {
                Debug.Log("申請を拒否しました");
                Reload();
            }
            else if (res == "failure")
            {
                Debug.Log("既に拒否された申請です");
                Reload();
            }
            else
            {
                Debug.Log(res);
            }
        }
    }


    // フレンド申請承認ボタン
    public void BtnApproval()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        GameObject parent = transform.parent.gameObject;
        Text id = parent.transform.Find("FriendID").gameObject.GetComponent<Text>();
        StartCoroutine(Approval(id.text, PlayerPrefs.GetString("ID")));
    }
    // フレンド申請承認処理
    public IEnumerator Approval(string id1, string id2)
    {
        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?application=approval&id1="+id1+"&id2="+id2;
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
            string res = request.downloadHandler.text;
            if (res == "success")
            {
                Debug.Log("申請を承認しました");
                Reload();
            }
            else if (res == "overlapped")
            {
                Debug.Log("重複する為、申請を削除しました");
                Reload();
            }
            else if (res == "failure")
            {
                Debug.Log("既に承認された申請です");
                Reload();
            }
            else
            {
                Debug.Log(res);
            }
        }
    }

    // リロード
    public void Reload()
    {
        btnFriend = GameObject.Find("Canvas/FriendBtn");
        friend friendComponent = btnFriend.GetComponent<friend>();
        StartCoroutine(friendComponent.checkApp(PlayerPrefs.GetString("ID")));
    }
}
