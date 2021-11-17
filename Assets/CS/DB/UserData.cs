using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class UserData
{
    public string id;
    public string name;

    public UserData(string id, string name)
    {
        this.id = id;
        this.name = name;
    }
}