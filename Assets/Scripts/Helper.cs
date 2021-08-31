using System;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    public static void Log(String text)
    {
        Debug.Log(text);
        GameObject.Find("Log").GetComponent<Text>().text = text;
    }
}