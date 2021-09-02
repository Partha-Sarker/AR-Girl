using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Cursor cursor;
    [SerializeField] private GameObject destroyButton;
    [SerializeField] private Image cursorImage;
    [SerializeField] private Sprite noCursorSprite, cursorSprite;

    public void OnActionClicked()
    {
        cursor.InstantiateObject();
    }

    public void OnDestroyClicked()
    {
        cursor.DestroyObject();
    }

    public void EnableDestroyButton()
    {
        destroyButton.SetActive(true);
    }

    public void DisableDestroyButton()
    {
        destroyButton.SetActive(false);
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
