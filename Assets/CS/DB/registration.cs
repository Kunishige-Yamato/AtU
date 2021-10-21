using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class registration: MonoBehaviour
{
    [SerializeField] InputField UserNameInput;
    [SerializeField] InputField PasswordInput;
    string id;
    public int returnID;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnSignUp()
    {
        bool check = true;
        Debug.Log(UserNameInput.text);
        if (!System.Text.RegularExpressions.Regex.IsMatch(UserNameInput.text, @"^[a-zA-Z0-9_]+$"))
        {
            check = false;
        }
        if (UserNameInput.text == "")
        {
            check = false;
        }
        if (check == true)
        {
            StartCoroutine(sign_up());
        }
    }

    public void btnSignIn()
    {
        id = "" + PlayerPrefs.GetInt("ID", 0);
        StartCoroutine(sign_in());
    }

    public IEnumerator sign_up()
    {
        if (UserNameInput.text != "" && PasswordInput.text != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("name", UserNameInput.text);
            form.AddField("pass", PasswordInput.text);

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
                Debug.Log(request.responseCode);
                returnID = int.Parse(request.downloadHandler.text);
                Debug.Log(returnID);
                PlayerPrefs.SetInt("ID", returnID);
                PlayerPrefs.Save();
            }
        }
    }

    public IEnumerator sign_in()
    {
        if (UserNameInput.text != "" && PasswordInput.text != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("name", UserNameInput.text);
            form.AddField("pass", PasswordInput.text);

            //UnityWebRequestを生成
            string url = "http://www.tmc-kkf.tokyo/sotsusei/request/index.php?sign_in=" + id;
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
                Debug.Log(request.responseCode);
                //returnID = int.Parse(request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text);
                Debug.Log(returnID);
                PlayerPrefs.SetInt("ID", returnID);
                PlayerPrefs.Save();
            }
        }
    }
}