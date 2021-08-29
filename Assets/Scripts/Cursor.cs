using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Cursor : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    private Camera _cam;
    private Vector3 _prevLocation, _prevRotation;
    [SerializeField] private GameObject objectToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var screenCenter = _cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var arRaycastHits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(screenCenter, arRaycastHits, TrackableType.Planes))
        {
            _prevLocation = arRaycastHits[0].pose.position;
            _prevRotation = arRaycastHits[0].pose.rotation.eulerAngles;
            transform.position = _prevLocation;
        }

        // if (!Input.GetMouseButtonDown(0)) return;
        // InstantiateObject();
    }

    public void InstantiateObject()
    {
        _prevRotation.x = 0;
        _prevRotation.z = 0;
        _prevRotation.y += 180;
        Instantiate(objectToSpawn, _prevLocation, Quaternion.Euler(_prevRotation));
    }
}
