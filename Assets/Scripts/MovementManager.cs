using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private Cursor cursor;
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
        // if (!_inputEnabled || _movingTarget == null)
        //     return;
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

        if (_movingTarget != null)
        {
            _movingTarget.eulerAngles = finalRotation;
            _movingTarget.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
        if (Cursor.animator != null)
            Cursor.animator.SetFloat("speed", inputVector.magnitude);
        Helper.Log(inputVector.normalized.magnitude);
    }
}