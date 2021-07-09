using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet18 : MonoBehaviour
{
    public Sprite image0;
    public Sprite image1;
    //落下速度
    float fallSpeed;
    //横移動速度
    float moveSpeed;

    void Start()
    {
        //スキンチェンジ
        if(transform.position.x>0){
            moveSpeed=-0.04f;
            this.gameObject.GetComponent<SpriteRenderer>().sprite=image0;
        }
        else{
            moveSpeed=0.04f;
            this.gameObject.GetComponent<SpriteRenderer>().sprite=image1;
        }

        fallSpeed=0.05f;
    }

    void FixedUpdate()
    {
		if(transform.position.y<-5.5) {
			Destroy (gameObject);
		}

        transform.Translate(moveSpeed,-fallSpeed,0,Space.World);
    }
}
