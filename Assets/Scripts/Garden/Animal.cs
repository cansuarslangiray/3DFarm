using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public string type;
    public bool isReady = false;
    public float growthTime;
    public bool isFeed = true;
    private GameObject _soil;
    private GameObject _player;
    private bool _secondTime = false;


    void Start()
    {
        _player = GameObject.Find("Player");
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(FeedCoroutine());
    }

    private IEnumerator FeedCoroutine()
    {
        while (true)
        {
            if (isFeed)
            {
                yield return new WaitForSeconds(growthTime);
                Prepared();
            }
            yield return null;
        }
    }


    private void Prepared()
    {
        isReady = true;
        isFeed = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Collection()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        isReady = false;
        isFeed = true;
        _player.GetComponent<Inventory>().IncreaseMilk();
        _secondTime = true;
        growthTime *= 2;
    }
}