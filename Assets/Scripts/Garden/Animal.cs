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
    
    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update()
    {
        if (isFeed)
        {
            Invoke("Prepared", growthTime);
        }

    }

    private void Prepared()
    {
        isReady = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
       // transform.GetComponent<TimeBarPlant>().GetSlider().SetActive(false);

    }
}
