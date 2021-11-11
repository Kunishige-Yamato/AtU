using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class registration: MonoBehaviour
{
    public InputField UserIdInput;
    public InputField UserNameInput;
    public InputField PasswordInput;
    string inputID;
    string inputName;
    string inputPass;
    string returnID;
    string returnName;

    // Start is called before the first frame update
    void Start()
    {
        // 自動ログイン。サインインの動作確認の間はコメントアウトで無効に。
        // StartCoroutine(connect());
        StartCoroutine(getEnemyInfo("1"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    // サインアップボタン押した時
    public void btnSignUp()
    {
        inputName = UserNameInput.text;
        inputPass = PasswordInput.text;
        bool check = true;

        if (!System.Text.RegularExpressions.Regex.IsMatch(inputName, @"^[a-zA-Z0-9_]+$"))
        {
            check = false;
            Debug.Log("名前だめー");
        }
        if (UserNameInput.text == "")
        {
            check = false;
        }
        if (check == true)
        {
            StartCoroutine(sign_up(inputName, inputPass));
        }
    }

    // サインインボタン押した時
    public void btnSignIn()
    {
        inputID = UserIdInput.text;
        inputPass = PasswordInput.text;
        StartCoroutine(sign_in(inputID, inputPass));
    }

    public IEnumerator connect()
    {
        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?connect=1";
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
            // PlayerPrefsのIDに9桁のIDが入っていればタイトル画面に飛ばす
            if (System.Text.RegularExpressions.Regex.IsMatch(PlayerPrefs.GetString("ID"), @"^[0-9]{9}$"))
            {
                SceneManager.LoadScene("title");
            }
        }
    }


    public IEnumerator sign_up(string name, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("pass", pass);

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?sign_up=1";
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            returnID = request.downloadHandler.text;
            if (System.Text.RegularExpressions.Regex.IsMatch(returnID, @"^[0-9]{9}$"))
            {
                // IDが9桁なら端末に保存
                PlayerPrefs.SetString("ID", returnID);
                PlayerPrefs.SetString("NAME", name);
                PlayerPrefs.Save();

                // IDが端末に正しく保存されていればシーンをタイトルへ
                if(System.Text.RegularExpressions.Regex.IsMatch(PlayerPrefs.GetString("ID"), @"^[0-9]{9}$"))
                {
                    SceneManager.LoadScene("title");
                }
                else
                {
                    Debug.Log("保存失敗");
                }
            }
            else
            {
                Debug.Log("ID取得失敗");
            }
        }
    }

    public IEnumerator sign_in(string id, string pass)
    {

        WWWForm form = new WWWForm();

        form.AddField("pass", pass);

        //UnityWebRequestを生成
        // idはパラメータに付加して送信、パスワードのみPOSTで送信
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?sign_in="+id;
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            returnName = request.downloadHandler.text;

            // ID値の不正入力やパスワードミスを処理
            if(returnName==null || returnName=="")
            {
                Debug.Log("入力に間違いがあります");
            }
            else
            {
                Debug.Log(returnName);
                PlayerPrefs.SetString("ID", id);
                PlayerPrefs.SetString("NAME", returnName);
                PlayerPrefs.Save();
                // セーブ出来ているかの確認方法は思いつき次第

                SceneManager.LoadScene("title");
            }
        }
    }

    public IEnumerator getEnemyInfo(string id)
    {
        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?enemy="+id;
        UnityWebRequest request = UnityWebRequest.Get(url);

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
            // return request.downloadHandler.text;
            Debug.Log(request.downloadHandler.text);
        }
    }
}