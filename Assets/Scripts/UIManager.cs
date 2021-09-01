using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Cursor cursor;
    [SerializeField] private Text spawnModeText;
    [SerializeField] private GameObject joyStick;

    private void Start()
    {
        switch (cursor.GetSpawnMode())
        {
            case Cursor.SpawnMode.Single:
                spawnModeText.text = "1";
                joyStick.SetActive(true);
                break;
            case Cursor.SpawnMode.Multiple:
                spawnModeText.text = "M";
                joyStick.SetActive(false);
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
                joyStick.SetActive(false);
                spawnModeText.text = "M";
                break;
            case Cursor.SpawnMode.Multiple:
                cursor.DestroyAllSpawnedObjects();
                cursor.SetSpawnMode(Cursor.SpawnMode.Single);
                joyStick.SetActive(true);
                spawnModeText.text = "1";
                break;
        }
    }

}
