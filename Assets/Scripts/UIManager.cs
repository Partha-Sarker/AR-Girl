using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Cursor cursor;
    [SerializeField] private Text spawnModeText;

    private void Start()
    {
        switch (cursor.GetSpawnMode())
        {
            case Cursor.SpawnMode.Single:
                spawnModeText.text = "1";
                break;
            case Cursor.SpawnMode.Multiple:
                spawnModeText.text = "M";
                break;
        }
    }

    public void SpawnObject()
    {
        cursor.InstantiateObject();
    }

    public void SwapSpawnMode()
    {
        switch (cursor.GetSpawnMode())
        {
            case Cursor.SpawnMode.Single:
                cursor.SetSpawnMode(Cursor.SpawnMode.Multiple);
                spawnModeText.text = "M";
                break;
            case Cursor.SpawnMode.Multiple:
                cursor.DestroyAllSpawnedObjects();
                cursor.SetSpawnMode(Cursor.SpawnMode.Single);
                spawnModeText.text = "1";
                break;
        }
    }
    
    
}
