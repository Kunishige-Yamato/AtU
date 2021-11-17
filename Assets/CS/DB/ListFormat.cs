using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListFormat: MonoBehaviour
{
    public UserData[] wr_list;      // ワールドランキング
    public UserData[] fr_list;      // フレンドランキング
    public UserData[] f_list;       // フレンドリスト
    public UserData[] app_list;     // フレンド申請リスト

    public void SetUserList(string[] receive, string type)
    {
        List<UserData> fromjson = new List<UserData>();
        for (int i = 0; i < receive.Length; i++)
        {
            fromjson.Add(JsonUtility.FromJson<UserData>(receive[i]));
        }
        switch (type)
        {
            case "wr":
                wr_list = fromjson.ToArray();
                break;
            case "fr":
                fr_list = fromjson.ToArray();
                break;
            case "fl":
                f_list = fromjson.ToArray();
                break;
            case "app":
                app_list = fromjson.ToArray();
                break;
        }
    }
}
