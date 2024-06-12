using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircle : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public int targetNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }

    public void YesButton()
    {
        player.GetComponent<Teleport>().TeleportTo(targetNum);
        canvas.SetActive(false);
    }

    public void NoButton()
    {
        canvas.SetActive(false);
    }
}