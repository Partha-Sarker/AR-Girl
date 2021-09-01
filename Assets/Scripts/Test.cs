using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private float speed = 5, rotationSpeed = 5;
    private Vector3 _input;

    // Update is called once per frame
    void Update()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.z = Input.GetAxis("Vertical");
        var offsetAngle = Mathf.Atan2(_input.x, _input.z) * Mathf.Rad2Deg;
        // Debug.Log(offsetAngle);

        var finalRotation = camTransform.eulerAngles;
        Debug.Log(finalRotation.ToString());
        finalRotation.y += offsetAngle;
        finalRotation.x = 0;
        finalRotation.z = 0;
        transform.eulerAngles = finalRotation;
        // transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, finalRotation, rotationSpeed * Time.deltaTime);
        transform.Translate(_input * speed * Time.deltaTime, Space.World);
    }
}
