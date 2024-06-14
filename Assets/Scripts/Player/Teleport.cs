using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget1;
    public Transform teleportTarget2;

    public void TeleportTo(int targetNum)
    {
        switch (targetNum)
        {
            case 1:
                transform.position = teleportTarget1.position;
                break;
            case 2:
                transform.position = teleportTarget2.position;
                break;
            default:
                Debug.LogWarning("!!!!");
                break;
        }
    }
}