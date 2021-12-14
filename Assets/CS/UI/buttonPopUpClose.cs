using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPopUpClose : MonoBehaviour
{
    public void OnClick()
    {
        Destroy(transform.parent.gameObject.transform.parent.gameObject);
    }
}
