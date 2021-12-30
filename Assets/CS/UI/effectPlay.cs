using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectPlay : MonoBehaviour
{
    //表示する画像群
    public Sprite[] sprites;
    //画像更新の速さ
    public float speed;
    //ループするかどうか
    public bool roop;

    //Imageコンポーネント用
    private Image image;
    //時間管理用
    private float current;

    void Start()
    {
        //image取得
        image = GetComponent<Image>();
        image.sprite = sprites[0];

        current = 0f;
        StartCoroutine(updateImg());
    }

    //エフェクト再生
    private IEnumerator updateImg()
    {
        int index = 0;
        if(roop)
        {
            while (true)
            {
                current += Time.deltaTime * speed;
                index = (int)(current) % sprites.Length;
                if (index >= sprites.Length - 1)
                {
                    image.color = new Color(1,1,1,0);
                    yield return new WaitForSeconds(0.1f);
                    image.color = new Color(1, 1, 1, 1);
                    continue;

                }
                image.sprite = sprites[index];
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (index < sprites.Length - 1)
            {
                current += Time.deltaTime * speed;
                index = (int)(current) % sprites.Length;
                if (index > sprites.Length - 1)
                {
                    index = sprites.Length - 1;
                }
                image.sprite = sprites[index];
                yield return new WaitForSeconds(0.01f);
            }

            Destroy(gameObject);
        }
    }
}
