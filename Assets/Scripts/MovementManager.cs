using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    private bool _inputEnabled;
    [SerializeField] private PlayerInput playerInput;
    private Transform _movingTarget;
    [Range(0, 100)]
    public float speed = 10, rotationSpeed = 10;
    public Transform MovingTarget
    {
        set => _movingTarget = value;
    }

    private void Update()
    {
        if (_movingTarget == null)
        {
            Debug.Log("Target null");
        }
        if (!_inputEnabled)
        {
            Debug.Log("Input disabled");
        }
        else
        {
            var camRotation = camTransform.eulerAngles;
            Helper.Log($"{camRotation.x} {camRotation.y} {camRotation.z}");
        }
    }

    public void EnableInput()
    {
        _inputEnabled = true;
    }

    public void DisableInput()
    {
        _inputEnabled = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!_inputEnabled || _movingTarget == null)
            return;
        var inputVector = context.ReadValue<Vector2>();

        var camRotation = camTransform.eulerAngles;
        
        camRotation.x = 0;
        camRotation.z = 0;

        _movingTarget.eulerAngles = Vector3.Lerp(_movingTarget.eulerAngles, camRotation, rotationSpeed * Time.deltaTime);
        _movingTarget.Translate(inputVector * speed * Time.deltaTime);
        
    }
}