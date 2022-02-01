using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class titleAdmin : MonoBehaviour
{
    public GameObject audioPrefab;
    public GameObject audioSEPrefab;
    AudioSource BGMSource, SESource;
    public AudioClip defaultClip;
    jsonReceive jsonRec;

    //ローディングのPrefab
    GameObject loadingPrefab;

    public GameObject popUpPrefab;
    GameObject popUp;

    //SE関係
    public AudioClip[] audioSEClips;

    //得点コード用
    public InputField inputField;
    string giftCode;

    //フレンドキャンバスの自分のIDのText
    public GameObject idTextObj;
    Text idText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SKIN") == false || PlayerPrefs.HasKey("TITLE") == false)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Other/LoadingCanvas");
            loadingPrefab = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
    }

    void Start()
    {
        //DB接続用
        jsonRec = new jsonReceive();

        GameObject mainAudio = GameObject.Find("AudioObj");
        GameObject mainSEAudio = GameObject.Find("AudioSEObj");

        //AudioSource作成
        if (mainAudio==null)
        {
            mainAudio = Instantiate(audioPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            mainAudio.name = "AudioObj";
            DontDestroyOnLoad(mainAudio);
        }
        if (mainSEAudio == null)
        {
            mainSEAudio = Instantiate(audioSEPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            mainSEAudio.name = "AudioSEObj";
            DontDestroyOnLoad(mainSEAudio);
        }

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

        if (BGMSource.isPlaying == false)
        {
            BGMSource.Play();
        }

        //スキンと称号の再セット
        if (PlayerPrefs.HasKey("SKIN") == false || PlayerPrefs.HasKey("TITLE") == false)  
        {
            StartCoroutine(SetDeco());
        }

        //フレンド人数確認、称号獲得チェック
        if (PlayerPrefs.GetInt("FRIEND_NUM") < 20)
        {
            StartCoroutine(jsonRec.CheckFriendNum(PlayerPrefs.GetString("ID")));
        }

        //得点コード用
        inputField = inputField.GetComponent<InputField>();

        //フレンドキャンバスに表示する自分のIDセット
        idText = idTextObj.GetComponent<Text>();
        idText.text = PlayerPrefs.GetString("ID");
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && popUp == null) 
        {
            //SE再生
            GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

            //ポップアップ作成，テキストとボタンの設定
            popUp = Instantiate(popUpPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //テキスト設定
            popUp.transform.Find("TextBackGround/Text").GetComponent<Text>().text = "Do you want to quit the game?";

            //ボタン1設定
            GameObject button_1 = popUp.transform.Find("Buttons/Button_1").gameObject;
            //ボタンの背景色設定
            button_1.GetComponent<Image>().color = Color.gray;
            //ボタンのテキスト変更
            button_1.transform.Find("Text").GetComponent<Text>().text = "Cancel";

            //ボタン2設定
            GameObject button_2 = popUp.transform.Find("Buttons/Button_2").gameObject;
            //ボタン押した時動くメソッドを追加
            button_2.GetComponent<Button>().onClick.AddListener(quit);
            //ボタンの背景色設定
            button_2.GetComponent<Image>().color = Color.red;
            //ボタンのテキスト変更
            button_2.transform.Find("Text").GetComponent<Text>().text = "Quit";

            //ボタンが1つでいい場合は「button_2」を取得後，以下を実行
            //Destroy(button_2);
        }
    }

    private void quit()
    {
        //ゲーム終了
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    public void InputGiftCode()
    {
        //入力内容確認
        giftCode = inputField.text;

        //DB接続用
        jsonReceive jsonRec = new jsonReceive();

        switch (giftCode)
        {
            case "uquJuMNia":
                //SE再生
                GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[1]);
                //トロフィー解放
                if (PlayerPrefs.HasKey("COMMAND_3") == false)
                {
                    StartCoroutine(jsonRec.SaveHiddenCommand(PlayerPrefs.GetString("ID"), 19));
                }
                break;
            case "OhSPoqfON":
                //SE再生
                GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[1]);
                //トロフィー解放
                if (PlayerPrefs.HasKey("COMMAND_4") == false)
                {
                    StartCoroutine(jsonRec.SaveHiddenCommand(PlayerPrefs.GetString("ID"), 20));
                }
                break;
            default:
                inputField.text = "";
                break;
        }
        Debug.Log(giftCode);
    }

    IEnumerator SetDeco()
    {
        yield return StartCoroutine(jsonRec.RecieveMyAchieve(PlayerPrefs.GetString("ID")));
        string[] mySetAchieve = jsonRec.GetMySetAchieve();
        yield return StartCoroutine(jsonRec.SaveDecoration(PlayerPrefs.GetString("ID"), int.Parse(mySetAchieve[0]), int.Parse(mySetAchieve[1])));

        Destroy(loadingPrefab.gameObject);
    }
}
