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
    public GameObject errorTextObj;
    Text errorText;
    string inputID;
    string inputName;
    string inputPass;
    string returnID;

    void Start()
    {
        // PlayerPrefsのIDに9桁のIDが入っていればタイトル画面に飛ばす
        if(System.Text.RegularExpressions.Regex.IsMatch(PlayerPrefs.GetString("ID"), @"^[0-9]{9}$"))
        {
            SceneManager.LoadScene("title");
        }

        //エラーテキスト用テキストコンポーネント取得
        errorText = errorTextObj.GetComponent<Text>();
        errorText.text = "";
    }

    void Update()
    {

    }

    // サインアップボタン押した時
    public void btnSignUp()
    {
        inputName = UserNameInput.text;
        inputPass = PasswordInput.text;
        bool check = true;
        Debug.Log(inputName);
        if (!System.Text.RegularExpressions.Regex.IsMatch(inputName, @"^[a-zA-Z0-9_]+$"))
        {
            check = false;
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


    public IEnumerator sign_up(string name, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("pass", pass);

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?sign_up=1";
        UnityWebRequest request = UnityWebRequest.Post(url, form);

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
            returnID = request.downloadHandler.text;
            PlayerPrefs.SetString("ID", returnID);
            PlayerPrefs.Save();
            SceneManager.LoadScene("title");
        }
    }

    public IEnumerator sign_in(string id, string pass)
    {

        WWWForm form = new WWWForm();
        // form.AddField("id", id); パラメータで解決
        form.AddField("pass", pass);

        //UnityWebRequestを生成
        string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?sign_in="+id;
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // SendWebRequestを実行して送受信開始
        yield return request.SendWebRequest();

        // isNetworkErrorとisHttpErrorでエラー判定
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            errorText.text = "Communication failed.";
        }
        else
        {
            returnID = request.downloadHandler.text;

            // パスワードミスをエラー処理
            if(!System.Text.RegularExpressions.Regex.IsMatch(returnID, @"^[0-9]{9}$"))
            {
                Debug.Log("ID,Pass違い\n"+returnID);
                errorText.text = "ID or password is wrong.";
            }
            else
            {
                PlayerPrefs.SetString("ID", returnID);
                PlayerPrefs.Save();

                // PlayerPrefsにセーブ出来ているかの確認if文
                // ログインはphp側で判定、IDが数字９桁でなければタイトルへの遷移は行わない
                if(System.Text.RegularExpressions.Regex.IsMatch(PlayerPrefs.GetString("ID"), @"^[0-9]{9}$"))
                {
                    SceneManager.LoadScene("title");
                }
                else
                {
                    Debug.Log("保存失敗");
                    errorText.text = "Failed to save.";
                }
            }
        }
    }
}