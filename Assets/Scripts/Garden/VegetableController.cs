using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableController : MonoBehaviour
{
    public float growthTime;

    private bool _isRipe;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Prepared", growthTime);
    }

    private void Prepared()
    {
        _isRipe = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    private void Harvest()
    {
        _isRipe = false;
    }
}