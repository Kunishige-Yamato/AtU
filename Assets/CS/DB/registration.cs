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

    public GameObject popUpPrefab;

    //ローディングのPrefab
    GameObject loadingPrefab;

    //BGM関係
    public GameObject audioPrefab;
    public GameObject audioSEPrefab;
    AudioSource BGMSource, SESource;
    public AudioClip defaultClip;

    //SE関係
    public AudioClip[] audioSEClips;

    private void Awake()
    {
        if (gameObject.name == "SignUpBtn")
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Other/LoadingCanvas");
            loadingPrefab = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
    }

    void Start()
    {
        // 自動ログイン。サインインの動作確認の間はコメントアウトで無効に。
        StartCoroutine(connect());
        StartCoroutine(getEnemyInfo("1"));

        GameObject mainAudio = GameObject.Find("AudioObj");
        GameObject mainSEAudio = GameObject.Find("AudioSEObj");
        if (mainAudio == null || mainSEAudio == null)
        {
            audioSourceCreate();
        }
    }

    void Update()
    {

    }

    void audioSourceCreate()
    {

        GameObject mainAudio = Instantiate(audioPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mainAudio.name = "AudioObj";
        DontDestroyOnLoad(mainAudio);
        
        GameObject mainSEAudio = Instantiate(audioSEPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mainSEAudio.name = "AudioSEObj";
        DontDestroyOnLoad(mainSEAudio);

        //コンポーネント取得
        BGMSource = mainAudio.GetComponent<AudioSource>();
        SESource = mainSEAudio.GetComponent<AudioSource>();

        //音量チェック
        //セーブされているかチェック
        if (PlayerPrefs.HasKey("BGMVOL"))
        {
            //既にセットされている場合
            BGMSource.volume = PlayerPrefs.GetFloat("BGMVOL");
        }
        else
        {
            //初めての場合
            BGMSource.volume = 0.5f;

        }
        //セーブされているかチェック
        if (PlayerPrefs.HasKey("SEVOL"))
        {
            //既にセットされている場合
            SESource.volume = PlayerPrefs.GetFloat("SEVOL");
        }
        else
        {
            //初めての場合
            SESource.volume = 0.5f;
        }

        BGMSource.clip = defaultClip;

        BGMSource.Play();
    }

    // サインアップボタン押した時
    public void btnSignUp()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[1]);

        //ポップアップ作成，テキストとボタンの設定
        GameObject popUp = Instantiate(popUpPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        popUp.name = "confPopup";

        //テキスト設定
        popUp.transform.Find("TextBackGround/Text").GetComponent<Text>().text = "You are creating a new user, are you sure?";

        //ボタン1設定
        GameObject button_1 = popUp.transform.Find("Buttons/Button_1").gameObject;
        //ボタンの背景色設定
        button_1.GetComponent<Image>().color = Color.gray;
        //ボタンのテキスト変更
        button_1.transform.Find("Text").GetComponent<Text>().text = "Cancel";

        //ボタン2設定
        GameObject button_2 = popUp.transform.Find("Buttons/Button_2").gameObject;
        //ボタン押した時動くメソッドを追加
        button_2.GetComponent<Button>().onClick.AddListener(SignUp);
        //ボタンの背景色設定
        button_2.GetComponent<Image>().color = Color.green;
        //ボタンのテキスト変更
        button_2.transform.Find("Text").GetComponent<Text>().text = "Sure";
    }

    //サインアップ機能
    public void SignUp()
    {
        inputName = UserNameInput.text;
        inputPass = PasswordInput.text;
        bool check = true;

        if (System.Text.RegularExpressions.Regex.IsMatch(inputName, @"('|\\)+"))
        {
            check = false;
            Debug.Log("不正な文字列が含まれています。");

            SignInPopup("Registration failed because the input content contains invalid characters.");
        }
        else if (inputName.Length < 1 || 20 < inputName.Length)
        {
            check = false;
            Debug.Log("名前は20文字まで使用できます。");

            SignInPopup("Use up to 20 characters in the name.");
        }
        else if (System.Text.RegularExpressions.Regex.IsMatch(inputPass, @"[^\da-zA-Z]+") || inputPass.Length < 8 || 16 < inputPass.Length)
        {
            check = false;
            Debug.Log("パスワードは8〜16文字の半角英数字で作成してください。");

            SignInPopup("Create the password using 8 to 16 single-byte alphanumerical characters.");
        }
        // チェックを終えて使用可能で合った場合
        if (check == true)
        {
            //SE再生
            GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);
            StartCoroutine(sign_up(inputName, inputPass));
        }
    }

    //名前とパスワードがエラー吐いた時のポップアップ作成
    public void SignInPopup(string message)
    {
        //既存のポップアップ削除
        Destroy(GameObject.Find("confPopup"));

        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[1]);

        //ポップアップ作成，テキストとボタンの設定
        GameObject popUp = Instantiate(popUpPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //テキスト設定
        popUp.transform.Find("TextBackGround/Text").GetComponent<Text>().text = message;

        //ボタン1設定
        GameObject button_1 = popUp.transform.Find("Buttons/Button_1").gameObject;
        //ボタンの背景色設定
        button_1.GetComponent<Image>().color = Color.gray;
        //ボタンのテキスト変更
        button_1.transform.Find("Text").GetComponent<Text>().text = "OK";

        //ボタン2設定
        GameObject button_2 = popUp.transform.Find("Buttons/Button_2").gameObject;
        Destroy(button_2);
    }

    //入力中
    public void InputText()
    {
        //エンターキーで確定
        if (UserNameInput.text.IndexOf("\n") == -1) 
        {
            return;
        }
        UserNameInput.text = UserNameInput.text.Replace("\n", "");
    }

    public void InputText2()
    {
        //エンターキーで確定
        if (PasswordInput.text.IndexOf("\n") == -1)
        {
            return;
        }
        PasswordInput.text = PasswordInput.text.Replace("\n", "");
    }

    // サインインボタン押した時
    public void btnSignIn()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

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
            if(loadingPrefab!=null)
            {
                Destroy(loadingPrefab.gameObject);
            }
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
                Text errorText = GameObject.Find("Canvas/SignInCanvas/ErrorText").GetComponent<Text>();
                errorText.text = "There is an error in the input.";
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
    }
}