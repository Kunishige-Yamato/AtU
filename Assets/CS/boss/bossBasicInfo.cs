using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossBasicInfo : MonoBehaviour
{
    //スキン
    SpriteRenderer skinSpriteRenderer;
    Sprite skinSprite;

    //体力
    int hp;
    int hit;

    //ボーナス
    int hitBonus;
    int defeatBonus;
    int timeBonus;

    //難易度，モード
    int[] modeDif;

    //UI
    Slider hpBar;

    //以下攻撃用
    //攻撃方法
    attack_boss atk;

    //ゲーム進行オブジェ
    GameObject progress;
    progress pro;

    //CSV関係
    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    //タイマー
    float timer;
    //攻撃一順時経過秒数保管用変数
    float lastTime;

    void Start()
    {
        //スキン設定
        skinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        skinSpriteRenderer.sprite = skinSprite;

        //hpバー設定
        hpBar = GameObject.Find("Slider").GetComponent<Slider>();
        hpBar.maxValue = hp;
        hpBar.value = hpBar.maxValue;

        //自分以外のEnemyタグのオブジェを削除してリセット
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject del in enemys)
        {
            if (del != gameObject)
            {
                Destroy(del);
            }
        }

        //進行用コンポーネント取得
        progress = GameObject.Find("Progress");
        pro = progress.GetComponent<progress>();

        //攻撃用コンポーネント取得
        atk = gameObject.GetComponent<attack_boss>();
        //モード，難易度取得
        modeDif = pro.GetDifficulty();
        atk.SetInfo(modeDif[0]);

        //タイマー初期化
        timer = 0;

        //csv読み込み
        csvFile = Resources.Load("CSV/"+name) as TextAsset;
        addList();
    }

    void FixedUpdate()
    {
        //時間計測
        timer += Time.deltaTime;

        //CSVを監視して秒数ごとに攻撃
        for (int i = 0; i < csvDatas.Count; i++)
        {
            if (float.Parse(csvDatas[i][1]) + lastTime <= timer && csvDatas[i][2] == "0")
            {
                Generate(csvDatas[i][0]);
                if (csvDatas[i][0] != "end")
                {
                    csvDatas[i][2] = "1";
                }
                else
                {
                    lastTime += float.Parse(csvDatas[i][1]);
                }
            }
        }
    }

    //設定読み込み
    public void SetBasicInfo(string name,int hp, int hitBonus, int defeatBonus, int timeBonus)
    {
        this.hp = hp;
        this.hitBonus = hitBonus;
        this.defeatBonus = defeatBonus;
        this.timeBonus = timeBonus;
        skinSprite = Resources.Load<Sprite>("Textures/"+name);
    }

    //エフェクト再生用
    void EffectAdd(float x, float y, string name)
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
        if (col.gameObject.tag == "Bullet")
        {
            hit++;

            hpBar.value = hp - hit;

            //スコア付与
            GameObject scoreCounter = GameObject.Find("ScoreCounter");
            ScoreCount sc = scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(10 + hitBonus * (modeDif[0] + 1));

            EffectAdd(col.transform.position.x,col.transform.position.y,"hitEffect");
            Destroy(col.gameObject);

            if (hit >= hp)
            {
                //爆発
                EffectAdd(transform.position.x, transform.position.y, "defeatEffect");
                //撃破ボーナス
                sc.AddScore(defeatBonus);
                //早期撃退ボーナス
                //sc.AddScore((int)Mathf.Floor(10000*(1+selectDifficulty.difficulty)/timer));
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

    //CSVをリスト形式で格納
    void addList()
    {
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み，リストに追加
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
    }

    //攻撃
    void Generate(string n)
    {
        switch (n)
        {
            case "s1":
                atk.Shoot1();
                break;
            case "s2":
                atk.Shoot2();
                break;
            case "s3":
                atk.Shoot3();
                break;
            case "end":
                addList();
                break;
        }
    }
}
