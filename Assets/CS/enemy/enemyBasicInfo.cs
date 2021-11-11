using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBasicInfo : MonoBehaviour
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

    //移動
    float fallSpeed;
    float moveSpeed;
    float rotSpeed;

    //設定読み込み
    public void SetBasicInfo(string name, int hp, int hitBonus, int defeatBonus, float fallSpeed, float moveSpeed, float rotSpeed)
    {
        this.hp = hp;
        this.hitBonus = hitBonus;
        this.defeatBonus = defeatBonus;
        this.fallSpeed = fallSpeed;
        this.moveSpeed = moveSpeed;
        this.rotSpeed = rotSpeed;
        skinSprite = Resources.Load<Sprite>("Textures/" + name);
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(moveSpeed, -fallSpeed, 0, Space.World);
        transform.Rotate(0, 0, rotSpeed);
        //端まで行ったら消去
        if (transform.position.y < -6f || transform.position.y > 6f || transform.position.x < -10f || transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }

    //エフェクト再生用
    void EffectAdd(float x, float y, string name)
    {
        GameObject explosionPrefab = Resources.Load("Prefabs/Effect/" + name) as GameObject;
        Vector3 pos = new Vector3(x, y, 0);
        GameObject canvas = GameObject.Find("EffectCanvas");
        GameObject g = Instantiate(explosionPrefab, pos, Quaternion.identity);
        g.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        g.transform.SetParent(canvas.transform, false);
        g.transform.position = pos;
    }

    //当たったら消去
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            hit++;

            //スコア付与
            GameObject scoreCounter = GameObject.Find("ScoreCounter");
            ScoreCount sc = scoreCounter.GetComponent<ScoreCount>();
            sc.AddScore(hitBonus);

            EffectAdd(col.transform.position.x, col.transform.position.y, "hitEffect");
            Destroy(col.gameObject);

            if (hit >= hp)
            {
                //爆発
                EffectAdd(transform.position.x, transform.position.y, "defeatEffect");
                //撃破ボーナス
                sc.AddScore(defeatBonus);
                Destroy(gameObject);
            }
        }
    }
}