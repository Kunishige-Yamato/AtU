using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    //選択された難易度
    int difficulty;

    //弾スキン
    public Sprite[] bulSkin;

    //弾関係
    public GameObject[] bulPrefab;
    //弾名前用
    int num;
    //弾射出元座標
    Vector3 bulPos;

    void Start()
    {
        //変数初期化
        num = 0;

        //弾射出元座標設定
        bulPos = new Vector3(transform.position.x, transform.position.y - 2, 0);
    }

    public void SetInfo(int difficulty)
    {
        this.difficulty = difficulty;
    }

    public void Shoot1()
    {
        if (difficulty != 3)
        {
            for (float i = -2; i <= 2; i += 0.1f)
            {
                //弾生成，名付け
                GameObject mb = Instantiate(bulPrefab[0], bulPos, Quaternion.identity);
                mb.name = "bullet_" + num;
                num++;

                //スキン設定
                mb.GetComponent<SpriteRenderer>().sprite = bulSkin[0] ;
                bullet8 b = mb.GetComponent<bullet8>();
                b.moveSpeed = i;
            }
        }
        else
        {
            for (float i = -2; i <= 2; i += 0.05f)
            {
                GameObject mb = Instantiate(bulPrefab[0], bulPos, Quaternion.identity);
                mb.name = "bullet_" + num;
                GameObject bul = GameObject.Find("bullet_" + num);
                num++;
                bullet8 b = bul.GetComponent<bullet8>();
                b.moveSpeed = i;
            }
        }
    }

    public void Shoot2()
    {

    }
    public void Shoot3()
    {

    }
}
