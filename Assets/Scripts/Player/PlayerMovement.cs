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
    private Animator _animator;
    private CharacterController _characterController;
    private PlayerControls _controls;
    private Vector2 _moveInput;
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    private Inventory _inventory;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _controls = new PlayerControls();
        _inventory = GetComponent<Inventory>();
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

    public void Move()
    {
      //  ApplyGravity();
        _animator.SetBool("IsWalking", true);
        transform.Rotate(0, _moveInput.x * 30 * Time.deltaTime, 0);

        if (_moveInput.y != 0)
        {
            _characterController.SimpleMove(transform.forward * movementSpeed);
        }
        else 
        {
            _animator.SetBool("IsWalking", false);
        }
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
            _characterController.SimpleMove(Vector3.up * _velocity);
        }
    }


    public void Harvest()
    {
        Debug.Log("heyyy");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Vegetable"))
            {
                _animator.SetBool("isPicked",true);
            }
        }
    }

    public void HarvestFinished()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Vegetable"))
            {
                VegetableController vegetable = hitCollider.GetComponentInParent<VegetableController>();
                if (vegetable != null && vegetable.isRipe)
                {
                    vegetable.isSeeded = false;
                    _inventory.AddVegetable(vegetable.gameObject);
                    Destroy(vegetable.gameObject);
                }
            }
        }
        _animator.SetBool("isPicked",false);

    }

    private void Plant()
    {
        if (_inventory.count > 0)
        {
            Debug.Log("1");
            GameObject plantPrefab = _inventory.GetPlantPrefab(Inventory.VegetableType.Tomato);
            if (plantPrefab != null)
            {
                Debug.Log("2");

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
                foreach (var hitCollider in hitColliders)
                {
                    Debug.Log("3");

                    if (hitCollider.gameObject.CompareTag("Soil"))
                    {
                        Debug.Log("4");

                        SoildManager soilManager = hitCollider.gameObject.GetComponent<SoildManager>();
                        if (!soilManager.isPlanted)
                        {
                            Debug.Log("5");

                            GameObject plant = Instantiate(plantPrefab, hitCollider.transform.position,
                                Quaternion.identity);
                            plant.SetActive(true);
                            soilManager.isPlanted = true;
                            _inventory.UseVegetable("Tomato");
                            break;
                        }
                    }
                }
            }
        }
    }
}