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
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.z = Input.GetAxisRaw("Vertical");
        
        if (_input.magnitude < .1)
            return;
        
        var offsetAngle = Mathf.Atan2(_input.x, _input.z) * Mathf.Rad2Deg;

        var finalRotation = camTransform.eulerAngles;
        finalRotation.y += offsetAngle;
        finalRotation.x = 0;
        finalRotation.z = 0;
        transform.eulerAngles = finalRotation;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
