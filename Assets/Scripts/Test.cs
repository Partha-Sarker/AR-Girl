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

        var camRotation = camTransform.eulerAngles;
        camRotation.x = 0;

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, camRotation, rotationSpeed * Time.deltaTime);
        transform.Translate(_input * speed * Time.deltaTime);
    }
}
