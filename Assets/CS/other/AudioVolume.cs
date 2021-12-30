using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    float b_vol, s_vol;

    AudioSource audioSource;
    Slider slider;

    void Start()
    {
        //BGM用AudioSource取得
        audioSource = GameObject.Find("AudioObj").GetComponent<AudioSource>();

        //自分のスライダー取得
        slider = gameObject.GetComponent<Slider>();

        if (gameObject.name == "BGM Slider")
        {
            //セーブされているかチェック
            if (PlayerPrefs.HasKey("BGMVOL"))
            {
                //既にセットされている場合
                b_vol = PlayerPrefs.GetFloat("BGMVOL");
            }
            else
            {
                //初めての場合
                b_vol = 1;

            }
            slider.value = b_vol;
        }

        if (gameObject.name == "SE Slider")
        {
            //セーブされているかチェック
            if (PlayerPrefs.HasKey("SEVOL"))
            {
                //既にセットされている場合
                s_vol = PlayerPrefs.GetFloat("SEVOL");
            }
            else
            {
                //初めての場合
                s_vol = 1;
            }
            slider.value = s_vol;
        }
    }

    void Update()
    {
        if (gameObject.name == "BGM Slider" && b_vol != slider.value)
        {
            b_vol = slider.value;
            PlayerPrefs.SetFloat("BGMVOL", b_vol);
            PlayerPrefs.Save();
        }
        else if (gameObject.name == "SE Slider" && s_vol != slider.value) 
        {
            s_vol = slider.value;
            PlayerPrefs.SetFloat("SEVOL", s_vol);
            PlayerPrefs.Save();
        }
    }
}
