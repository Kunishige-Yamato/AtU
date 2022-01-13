using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    }

    void Update()
    {
        
    }

    IEnumerator SetDeco()
    {
        yield return StartCoroutine(jsonRec.RecieveMyAchieve(PlayerPrefs.GetString("ID")));
        string[] mySetAchieve = jsonRec.GetMySetAchieve();
        yield return StartCoroutine(jsonRec.SaveDecoration(PlayerPrefs.GetString("ID"), int.Parse(mySetAchieve[0]), int.Parse(mySetAchieve[1])));

        Destroy(loadingPrefab.gameObject);
    }
}
