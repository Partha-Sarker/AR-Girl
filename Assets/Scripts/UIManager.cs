using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Cursor cursor;
    [SerializeField] private GameObject joyStick;
    [SerializeField] private Image cursorImage;
    [SerializeField] private Sprite noCursorSprite, cursorSprite;

    public void SpawnObject()
    {
        cursor.InstantiateObject();
    }

    public void ToggleCursor()
    {
        if (cursor.IsCursorVisible())
        {
            cursorImage.sprite = cursorSprite;
            cursor.HideCursor();
        }
        else
        {
            cursorImage.sprite = noCursorSprite;
            cursor.ShowCursor();
        }
    }

}
