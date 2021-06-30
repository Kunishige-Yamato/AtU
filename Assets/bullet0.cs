using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullet0 : MonoBehaviour
{
    public Sprite image0;
    public Sprite image1;
    GameObject player;
    player pl;

    //爆発エフェクトのPrefab
	public GameObject explosionPrefab;

    void Start()
    {
        player=GameObject.Find("Player");
        pl=player.GetComponent<player>();
        changeSprite();
    }

    void FixedUpdate()
    {
        transform.Translate(0,0.3f,0);

		if (transform.position.y>5.5) {
			Destroy (gameObject);
		}
    }

    void changeSprite()
    {
        if(pl.imageNum==0){
            this.gameObject.GetComponent<SpriteRenderer>().sprite=image0;
        }
        else{
            this.gameObject.GetComponent<SpriteRenderer>().sprite=image1;
        }
    }
    public void explosion()
    {
        // 爆発エフェクトを生成する	
        Instantiate (explosionPrefab, transform.position, Quaternion.identity);
    }
}
