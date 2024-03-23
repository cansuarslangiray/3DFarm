using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * -1;
        float moveVertical = Input.GetAxis("Vertical") * -1;
        float jump;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = 1;
        }
        else
        {
            jump = 0;
        }


        Vector3 movement = new Vector3(moveHorizontal, jump, moveVertical);


        transform.Translate(movement * moveSpeed * Time.deltaTime);
        if (moveHorizontal != 0)
        {
            float rotationAmount = moveHorizontal * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }
    }
}