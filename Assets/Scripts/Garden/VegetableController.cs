using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VegetableController : MonoBehaviour
{
    public float growthTime;

    public bool isRipe = false;
    public bool isSeeded = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSeeded)
        {
            Invoke("Prepared", growthTime);
        }
    }

    private void Prepared()
    {
        isRipe = true;
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}