using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPopUpClose : MonoBehaviour
{
    //SE関係
    public AudioClip[] audioSEClips;

    public void OnClick()
    {
        //SE再生
        GameObject.Find("AudioSEObj").GetComponent<AudioSource>().PlayOneShot(audioSEClips[0]);

        Destroy(transform.parent.gameObject.transform.parent.gameObject);
    }
}
