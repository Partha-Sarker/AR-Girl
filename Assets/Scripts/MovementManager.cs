using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private Cursor cursor;
    [SerializeField] private Transform camTransform;
    private Transform _movingTarget;
    private Animator _animator;
    [Range(0, 20)]
    public float speed = 10;

    private void Start()
    {
        cursor.OnObjectSpawn += SetGameObjectAndAnimator;
        cursor.OnObjectDestroy += ResetGameObjectAndAnimator;
    }

    public Transform MovingTarget
    {
        set => _movingTarget = value;
    }

    public void SetGameObjectAndAnimator(GameObject currentObject)
    {
        _movingTarget = currentObject.transform;
        _animator = currentObject.GetComponent<Animator>();
    }

    public void ResetGameObjectAndAnimator()
    {
        _movingTarget = null;
        _animator = null;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (_movingTarget == null)
            return;
        
        var inputVector = context.ReadValue<Vector2>();

        if (inputVector.magnitude < .1)
        {
            inputVector.x = 0;
            inputVector.y = 0;
        }

        if (inputVector.magnitude > 1)
            inputVector = inputVector.normalized;

        var offsetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
        var finalRotation = camTransform.eulerAngles;
        
        finalRotation.y += offsetAngle;
        finalRotation.x = 0;
        finalRotation.z = 0;

        _movingTarget.eulerAngles = finalRotation;
        _movingTarget.Translate(Vector3.forward * speed * Time.deltaTime);
        
        _animator.SetFloat("speed", inputVector.magnitude);
    }
}