using System;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    public static void Log <T> (T text)
    {
        Debug.Log(text);
        GameObject.Find("Log").GetComponent<Text>().text = text.ToString();
    }
}