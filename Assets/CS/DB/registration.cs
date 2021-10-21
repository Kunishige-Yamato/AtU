using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class registration: MonoBehaviour
{
    [SerializeField] InputField UserIdInput;
    [SerializeField] InputField UserNameInput;
    [SerializeField] InputField PasswordInput;
    string inputID;
    string inputName;
    string inputPass;
    int returnID;

    // Start is called before the first frame update
    void Start()
    {

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
            Debug.Log(request.downloadHandler.text);
            returnID = int.Parse(request.downloadHandler.text);
            Debug.Log(returnID);
            PlayerPrefs.SetInt("ID", returnID);
            PlayerPrefs.Save();
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
        }
        else
        {
            Debug.Log(request.responseCode);
            returnID = int.Parse(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            Debug.Log(returnID);
            PlayerPrefs.SetInt("ID", returnID);
            PlayerPrefs.Save();
        }
    }
}