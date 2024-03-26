using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    [SerializeField] private float speed;

    [Header("Animation")]
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        // Reads Input
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);

        // Adjust direction for Isometric view
        _direction = Quaternion.Euler(0, 45, 0) * _direction;

        // Normalize direction
        _direction.Normalize();


        if (_direction != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }
}
