using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bossBasicInfo : MonoBehaviour
{
    //スキン
    SpriteRenderer skinSpriteRenderer;
    public Sprite[] skinSprite;

    //選択された難易度
    int difficulty,endless;

    //体力
    int hp;
    public int hit;

    //ボーナス
    int hitBonus;
    int defeatBonus;
    int timeBonus;

    //UI
    Slider hpBar;

    //ゲーム進行オブジェ
    GameObject progress;
    progress pro;

    //難易度，モード
    int[] modeDif;

    //タイマー
    float timer;

    //BGM関係
    AudioSource audioSource;
    public AudioClip audioClip;
    float fadeInSeconds = 1.0f, fadeOutSeconds = 1.0f, fadeDeltaTime, fadeDeltaTime2;
    bool isFadeIn = false, bgmChange = false;

    //SE関係
    AudioSource audioSESource;
    public AudioClip[] audioSEClips;

    void Start()
    {
        //進行用コンポーネント取得
        progress = GameObject.Find("Progress");
        pro = progress.GetComponent<progress>();

        //モード，難易度取得
        modeDif = pro.GetDifficulty();
        difficulty = modeDif[0];
        endless = modeDif[1];

        //hpバー設定
        if(endless==0)
        {
            hpBar = GameObject.Find("StoryHP/Slider").GetComponent<Slider>();
        }
        else
        {
            hpBar = GameObject.Find("EndlessHP/Slider").GetComponent<Slider>();
        }
        hpBar.maxValue = hp;
        hpBar.value = hpBar.maxValue;

        //タイマー初期化
        timer = 0;

        //自分以外のEnemyタグのオブジェを削除してリセット
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys)
        {
            if (del != gameObject)
            {
                Destroy(del);
            }
        }

        //表示レイヤーを後ろに
        SpriteRenderer sr=gameObject.GetComponent<SpriteRenderer>();
        sr.sortingOrder = -1;

        //BGMをボス専用に切り替え
        if(audioClip!=null)
        {
            GameObject mainAudio = GameObject.Find("AudioObj");
            audioSource = mainAudio.GetComponent<AudioSource>();
            bgmChange = true;
        }

        //SE再生用コンポーネント取得
        audioSESource = GameObject.Find("AudioSEObj").GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        //時間計測
        timer += Time.deltaTime;

        //BGMフェードイン
        if (isFadeIn)
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeInSeconds)
            {
                fadeDeltaTime = fadeInSeconds;
                isFadeIn = false;
            }
            audioSource.volume = (float)(fadeDeltaTime / fadeInSeconds) * PlayerPrefs.GetFloat("BGMVOL");
        }
        else
        {
            fadeDeltaTime = 0;
        }

        //BGMフェードアウト
        if (bgmChange)
        {
            fadeDeltaTime2 += Time.deltaTime;
            if (fadeDeltaTime2 >= fadeOutSeconds)
            {
                fadeDeltaTime2 = fadeOutSeconds;
                bgmChange = false;
                //BGM変更
                audioSource.clip = audioClip;
                audioSource.Play();
                isFadeIn = true;
            }
            audioSource.volume = (float)(1.0 - fadeDeltaTime2 / fadeOutSeconds) * PlayerPrefs.GetFloat("BGMVOL");
        }
        else
        {
            fadeDeltaTime2 = 0;
        }
    }

    //設定読み込み
    public void SetBasicInfo(string name,int hp, int hitBonus, int defeatBonus, int timeBonus)
    {
        gameObject.name = name;
        this.hp = hp;
        this.hitBonus = hitBonus;
        this.defeatBonus = defeatBonus;
        this.timeBonus = timeBonus;
        SetSkin(0);
    }

    //設定書き出し
    public int[] GetBasicInfo()
    {
        int[] basicInfo=new int[4];
        basicInfo[0] = hp;
        basicInfo[1] = hitBonus;
        basicInfo[2] = defeatBonus;
        basicInfo[3] = timeBonus;

        return basicInfo;
    }

    //エフェクト再生用
    public void EffectAdd(float x, float y, string name)
    {
        GameObject explosionPrefab = Resources.Load("Prefabs/Effect/" + name) as GameObject;
        Vector3 pos = new Vector3(x, y, 0);
        GameObject canvas = GameObject.Find("EffectCanvas");
        GameObject g = Instantiate(explosionPrefab, pos, Quaternion.identity);
        g.transform.SetParent(canvas.transform, false);
        g.transform.position = pos;
    }

    //被弾時
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet" && timer >= 0.2f)
        {
            hit++;

            hpBar.value = hp - hit;

            //スコア付与
            GameObject scoreCounter = GameObject.Find("ScoreCounter");
            ScoreCount sc = scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(10 + hitBonus * (modeDif[0] + 1));

            //被弾演出
            EffectAdd(col.transform.position.x,col.transform.position.y,"hitEffect");
            audioSESource.PlayOneShot(audioSEClips[0]);

            Destroy(col.gameObject);

            if (hit >= hp)
            {
                //爆発
                EffectAdd(transform.position.x, transform.position.y, "defeatEffect");
                audioSESource.PlayOneShot(audioSEClips[1]);

                //撃破ボーナス
                sc.AddScore(defeatBonus);
                //早期撃退ボーナス
                if(timer<=timeBonus)
                {
                    sc.AddScore(timeBonus-(int)timer);
                }

                //次のステージへ
                if (modeDif[1]==1)
                {
                    pro.DisplayResult();
                }
                else
                {
                    pro.DisplayResult();
                }
                Destroy(gameObject);
            }
        }
    }

    //複数ボスの場合2人の体力を等しくする
    public void EqualHp(GameObject buddyObj)
    {
        bossBasicInfo buddyBasicInfo = buddyObj.GetComponent<bossBasicInfo>();
        buddyBasicInfo.hit = hit;
        //死んでたらバディも撃破
        if (buddyBasicInfo.hit >= hp) 
        {
            buddyBasicInfo.EffectAdd(buddyObj.transform.position.x, buddyObj.transform.position.y, "defeatEffect");
            Destroy(buddyObj);
        }
    }

    public void SetSkin(int skinNum)
    {
        if (skinNum < skinSprite.Length && skinSprite[skinNum] != null)
        {
            skinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            skinSpriteRenderer.sprite = skinSprite[skinNum];
        }
        else
        {
            Debug.Log(skinNum + "番にはspriteが設定されていません");
        }
    }
}