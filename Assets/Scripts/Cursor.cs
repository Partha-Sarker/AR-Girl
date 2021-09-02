using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Cursor : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private MovementManager movementManager;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private MeshRenderer cursorRenderer;
    
    public UnityEvent onCursorAvailable, onCursorLost;
    public delegate void VoidGameObject(GameObject s);
    public delegate void VoidVoid();
    public VoidGameObject OnObjectSpawn;
    public VoidVoid OnObjectDestroy;
    
    public bool isCursorAvailable;
    public Vector3 prevLocation, prevRotation;
    
    private GameObject _currentObject;
    
    private void Update()
    {
        var screenCenter = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var arRaycastHits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(screenCenter, arRaycastHits, TrackableType.Planes))
        {
            if (!isCursorAvailable)
            {
                isCursorAvailable = true;
                onCursorAvailable.Invoke();
            }
            prevLocation = arRaycastHits[0].pose.position;
            prevRotation = arRaycastHits[0].pose.rotation.eulerAngles;
            transform.position = prevLocation;
        }
        else if (isCursorAvailable)
        {
            isCursorAvailable = false;
            onCursorLost.Invoke();
        }
    }

    public void InstantiateObject()
    {
        prevRotation.x = 0;
        prevRotation.z = 0;
        prevRotation.y += 180;
        if (_currentObject != null)
        {
            _currentObject.transform.position = prevLocation;
            _currentObject.transform.eulerAngles = prevRotation;
        }
        else
        {
            _currentObject = Instantiate(objectToSpawn, prevLocation, Quaternion.Euler(prevRotation));
            OnObjectSpawn(_currentObject);
        }
        movementManager.MovingTarget = _currentObject.transform;
    }

    public void DestroyObject()
    {
        if (_currentObject == null)
            return;
        Destroy(_currentObject);
        OnObjectDestroy();
    }

    public bool IsCursorVisible()
    {
        return cursorRenderer.enabled;
    }

    public void ShowCursor()
    {
        cursorRenderer.enabled = true;
    }

    public void HideCursor()
    {
        cursorRenderer.enabled = false;
    }
}
