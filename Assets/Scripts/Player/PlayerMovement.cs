using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    public Transform cam;

    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.F))
        {
            Harvest();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _controller.Move(moveDir.normalized * speed * Time.deltaTime);
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
                    Debug.Log('j');
                }
            }
        }
    }
}