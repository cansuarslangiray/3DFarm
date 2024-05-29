using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget; 
    
    public void Teleportation()
    {
        transform.position = teleportTarget.position;
    }
}
