using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical")*-1 ;
        float jump;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = 1;
        }
        else
        {
            jump = 0;
        }


            Vector3 movement = new Vector3(moveHorizontal, jump,moveVertical);
       
        
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
