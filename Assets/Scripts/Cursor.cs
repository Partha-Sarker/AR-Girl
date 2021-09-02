using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Cursor : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private MeshRenderer cursorRenderer;
    
    public UnityEvent onCursorAvailable, onCursorLost, onObjectSpawnUnity, onObjectDestroyUnity;
    public delegate void VoidGameObject(GameObject s);
    public delegate void VoidVoid();
    public VoidGameObject OnObjectSpawn;
    public VoidVoid OnObjectDestroy;
    
    private bool _isCursorAvailable;
    private Vector3 _prevLocation, _prevRotation;
    
    private GameObject _currentObject;
    
    private void Update()
    {
        var screenCenter = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var arRaycastHits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(screenCenter, arRaycastHits, TrackableType.Planes))
        {
            if (!_isCursorAvailable)
            {
                _isCursorAvailable = true;
                onCursorAvailable.Invoke();
            }
            _prevLocation = arRaycastHits[0].pose.position;
            _prevRotation = arRaycastHits[0].pose.rotation.eulerAngles;
            transform.position = _prevLocation;
        }
        else if (_isCursorAvailable)
        {
            _isCursorAvailable = false;
            onCursorLost.Invoke();
        }
    }

    public void InstantiateObject()
    { 
        _prevRotation.x = 0;
        _prevRotation.z = 0;
        _prevRotation.y += 180;
        if (_currentObject != null)
        {
            _currentObject.transform.position = _prevLocation;
            _currentObject.transform.eulerAngles = _prevRotation;
        }
        else
        {
            _currentObject = Instantiate(objectToSpawn, _prevLocation, Quaternion.Euler(_prevRotation));
            OnObjectSpawn(_currentObject);
            onObjectSpawnUnity.Invoke();
        }
    }

    public void DestroyObject()
    {
        if (_currentObject == null)
            return;
        Destroy(_currentObject);
        OnObjectDestroy();
        onObjectDestroyUnity.Invoke();
    }

    public bool IsCursorVisible()
    {
        return cursorRenderer.enabled;
    }

    public bool IsCurrentObjectNull()
    {
        return _currentObject == null;
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
