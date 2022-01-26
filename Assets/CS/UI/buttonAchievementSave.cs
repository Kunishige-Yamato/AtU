using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonAchievementSave : MonoBehaviour
{
    jsonReceive jsonRec = new jsonReceive();
    int s_num, t_num, s_num_def, t_num_def;

    //確認用ポップアップ
    public GameObject popUpPrefab;

    //SE関係
    public AudioClip[] audioSEClips;

    void Start()
    {

    }

    void Update()
    {
        
    }

    //最初にセットされてるやつ取得
    public void SetDefSkinNum(int skinNum)
    {
        s_num_def = skinNum;
        SetSkinNum(s_num_def);
    }

    public void SetDefTitleNum(int titleNum)
    {
        t_num_def = titleNum;
        SetTitleNum(t_num_def);
    }

    //変更した場合番号取得
    public void SetSkinNum(int skinNum)
    {
        s_num = skinNum;
    }

    public void SetTitleNum(int titleNum)
    {
        t_num = titleNum;
    }

    public int[] GetDefaultDeco()
    {
        int[] deco = { s_num_def, t_num_def };
        return deco;
    }

    public void SaveDecoration()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        //変化があった場合セーブ
        if (s_num!=s_num_def||t_num!=t_num_def)
        {
            Debug.Log(t_num + "," + s_num);
            StartCoroutine(jsonRec.SaveDecoration(PlayerPrefs.GetString("ID"),t_num,s_num));
            SetDefSkinNum(s_num);
            SetDefTitleNum(t_num);
            buttonAchievementSave btnTitleBack = GameObject.Find("Canvas/BackTitleButton").GetComponent<buttonAchievementSave>();
            btnTitleBack.SetDefSkinNum(s_num);
            btnTitleBack.SetDefTitleNum(t_num);
        }
        else
        {
            Debug.Log("not change");
        }
    }

    public void BackTitleBtn()
    {
        if (s_num != s_num_def || t_num != t_num_def)
        {
            //ポップアップ作成，テキストとボタンの設定
            GameObject popUp = Instantiate(popUpPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //テキスト設定
            popUp.transform.Find("TextBackGround/Text").GetComponent<Text>().text = "You haven't confirmed the changes you made, are you really sure you want to go back to the title?";

            //ボタン1設定
            GameObject button_1 = popUp.transform.Find("Buttons/Button_1").gameObject;
            //ボタンの背景色設定
            button_1.GetComponent<Image>().color = Color.gray;
            //ボタンのテキスト変更
            button_1.transform.Find("Text").GetComponent<Text>().text = "Cancel";

            //ボタン2設定
            GameObject button_2 = popUp.transform.Find("Buttons/Button_2").gameObject;
            //ボタン押した時動くメソッドを追加
            button_2.GetComponent<Button>().onClick.AddListener(BackTitle);
            //ボタンの背景色設定
            button_2.GetComponent<Image>().color = Color.red;
            //ボタンのテキスト変更
            button_2.transform.Find("Text").GetComponent<Text>().text = "Back Title";

            //SE再生
            GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[1]);
        }
        else
        {
            BackTitle();
        }
    }

    private void BackTitle()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);
        //サインアップシーン移動
        SceneManager.LoadScene("title");
    }
}
