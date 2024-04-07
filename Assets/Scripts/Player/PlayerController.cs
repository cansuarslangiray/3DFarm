using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     private Animator animator;

  public float walkSpeed = 2.0f;
  public float turnSpeed = 1.5f;
  [SerializeField] private float runSpeed = 5f;
  private float horizontalInput;
  private CharacterController _controller;

  void Start()
  {
    
    _controller = transform.GetComponent<CharacterController>();

  }

  void Update()
  {
    Walk();
    Turn();
  }



  private void Walk()
  {
    

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    {
    
      Turn();
      _controller.Move(-transform.forward * walkSpeed * Time.deltaTime);

    }
    else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    {
      Turn();
      _controller.Move(transform.forward * walkSpeed * Time.deltaTime);
    }

  
  }
  
  private void Turn()
  {
    if (Input.GetKey(KeyCode.A))
    {
      // turn left
      transform.Rotate(0, -90 * Time.deltaTime, 0);

    }
    else if (Input.GetKey(KeyCode.D))
    {
      // turn right
      transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

  }

}