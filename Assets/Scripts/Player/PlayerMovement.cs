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
    private String _tyepVeg;
    private bool _canMove = true;
    public GameObject optionsVeg;
    public GameObject vegContanier;
    private float _money=10000;
    private bool _isUsed=false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _controls = new PlayerControls();
        _inventory = GetComponent<Inventory>();
        _controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _controls.Player.Havrst.performed += ctx => Harvest();
        _controls.Player.Plant.performed += ctx => ActivateOptions();
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
        if (transform.position.y < 5)
        {
            Vector3 position = new Vector3(10, 9.2f, 22);
            transform.position = position;
        }

        Move();
    }

    public void Move()
    {
        if (!_canMove) return;

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


    public void Harvest()
    {
        Debug.Log("heyyy");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Vegetable"))
            {
                _isUsed = true;
                _animator.SetBool("isPicked", true);
                Collider[] hitSoil = Physics.OverlapSphere(transform.position, 1.0f);
                foreach (var soil in hitSoil)
                {
                    if (soil.gameObject.CompareTag("Soil"))
                    {
                        soil.GetComponent<SoildManager>().isPlanted = false;
                    }
                }
            }
        }

        if (_isUsed)
        {
            _canMove = false;
            _isUsed = false;
        }
        else
        {
            _canMove = true;
            _isUsed = true;
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

        _animator.SetBool("isPicked", false);
        _canMove = true;
    }

    public void ActivateOptions()
    {
        optionsVeg.SetActive(true);
    }

    public void Plant()
    {
      
        Debug.Log("1");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Soil"))
            {
                _isUsed = true;
                SoildManager soilManager = hitCollider.gameObject.GetComponent<SoildManager>();
                if (!soilManager.isPlanted)
                {
                    _animator.SetBool("isPlanted", true);
                }
            }
        }
        if (_isUsed)
        {
            _canMove = false;
            _isUsed = false;
        }
        else
        {
            _canMove = true;
            _isUsed = true;
        }
    }

    public void PlantFinshed()
    {
        Debug.Log("1223");
        Inventory.VegetableType typeVeg;
        if (Enum.TryParse(_tyepVeg, true, out typeVeg))
        {
            GameObject plantPrefab = _inventory.GetPlantPrefab(typeVeg);
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
                            plant.transform.SetParent(vegContanier.transform);
                            plant.SetActive(true);
                            soilManager.isPlanted = true;
                            _inventory.UseVegetable(_tyepVeg);
                            _animator.SetBool("isPlanted", false);
                            _canMove = true;
                            break;
                        }
                    }
                }
            }
        }

        _canMove = true;
        _animator.SetBool("isPlanted", false);
    }

    public void SetVeg(String type)
    {
        _tyepVeg = type;
    }

    public float GetMoney()
    {
        return _money;
    }

    public void SetMoney(float money)
    {
        _money = money;
    }
}