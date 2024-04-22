using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;

    private CharacterController _characterController;
    private PlayerControls _controls;
    private Vector2 _moveInput;
    private Transform _cameraTransform;
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _controls = new PlayerControls();
        _cameraTransform = Camera.main.transform;

        _controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _controls.Player.Havrst.performed += ctx => Harvest();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        
        Move();
    }


    private void Move()
    {
        if (_characterController.isGrounded)
        {
            Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;

            if (moveDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg +
                                    _cameraTransform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

                Vector3 forward = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _characterController.Move(forward.normalized * movementSpeed * Time.deltaTime);
            }
        }
        else if (!_characterController.isGrounded)
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
            _characterController.Move(new Vector3(0, _velocity * Time.deltaTime, 0));
        }
    }


    private void Harvest()
    {
        var inventory = gameObject.GetComponent<Inventory>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Vegetable"))
            {
                VegetableController vegetable = hitCollider.GetComponentInParent<VegetableController>();
                if (vegetable != null && vegetable.isRipe)
                {
                    vegetable.isSeeded = false;
                    inventory.AddVegetable(vegetable.gameObject);
                    Destroy(vegetable.gameObject);
                }
            }
        }
    }
}