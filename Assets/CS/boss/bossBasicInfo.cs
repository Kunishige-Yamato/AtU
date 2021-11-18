using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bossBasicInfo : MonoBehaviour
{
    //選択された難易度
    int difficulty;

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

    //UI
    Slider hpBar;

    //ゲーム進行オブジェ
    GameObject progress;
    progress pro;

    //難易度，モード
    int[] modeDif;

    //タイマー
    float timer;

    void Start()
    {
        //スキン設定
        skinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        skinSpriteRenderer.sprite = skinSprite;

        //進行用コンポーネント取得
        progress = GameObject.Find("Progress");
        pro = progress.GetComponent<progress>();

        //モード，難易度取得
        modeDif = pro.GetDifficulty();
        difficulty = modeDif[0];

        //hpバー設定
        hpBar = GameObject.Find("HP/Slider").GetComponent<Slider>();
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
    }

    void FixedUpdate()
    {
        //時間計測
        timer += Time.deltaTime;
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
        if (col.gameObject.tag == "Bullet" && timer >= 0.2f)
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
}
