using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_boss : MonoBehaviour
{
    //選択された難易度
    int difficulty;

    //弾スキン
    public Sprite[] bulSkin;

    //弾関係
    public GameObject[] bulPrefab;
    //弾名前用
    int num,num2;
    //弾射出元座標
    Vector3 bulPos,bulPos2;

    //ビームの出す方向
    bool dir;
    //左右にビーム振るのに必要な変数
    float mov, maxMov, minMov;
    //弾の生成数
    int count = 0;


    void Start()
    {
        //変数初期化
        num = 0;
        num2 = 0;
        mov = -1;
        maxMov = mov * -1;
        minMov = mov;
        dir = false;

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
                //mb.GetComponent<SpriteRenderer>().sprite = bulSkin[0] ;

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

        if (mov <= maxMov && dir == false)
        {
            GameObject mb2 = Instantiate(bulPrefab[1], bulPos, Quaternion.identity);
            mb2.name = "bullet_" + num2;
            GameObject bul2 = GameObject.Find("bullet_" + num2);
            num2++;
            bullet9 b2 = bul2.GetComponent<bullet9>();
            b2.moveSpeed = mov / 10;
            mov += 0.03f;
            Invoke("Shoot2", 0.06f);
        }
        else if (dir == false)
        {
            Invoke("switchDir", 1);
        }
        if (mov >= minMov && dir == true)
        {
            GameObject mb2 = Instantiate(bulPrefab[1], bulPos, Quaternion.identity);
            mb2.name = "bullet_" + num2;
            GameObject bul2 = GameObject.Find("bullet_" + num2);
            num2++;
            bullet9 b2 = bul2.GetComponent<bullet9>();
            b2.moveSpeed = mov / 10;
            mov -= 0.03f;
            Invoke("Shoot2", 0.06f);
        }
        else if (dir == true)
        {
            Invoke("switchDir", 1);
        }
    }

    void switchDir()
    {
        if (dir == false)
        {
            dir = true;
            Shoot2();
        }
        else
        {
            dir = false;
        }
    }

    public void Shoot3()
    {
        bulPos2.x = Random.Range(-8f, 8f);
        bulPos2.y = 5.5f;
        Instantiate(bulPrefab[2], bulPos2, Quaternion.identity);
        count++;

        if (count < 50 && selectDifficulty.difficulty != 3)
        {
            Invoke("Shoot3", Random.Range(0.1f, 0.3f));
        }
        else if (count < 100 && selectDifficulty.difficulty == 3)
        {
            Invoke("Shoot3", Random.Range(0.05f, 0.15f));
        }
        else
        {
            count = 0;
        }
    }
}

/*
 * boss2:bul11~15
 */
