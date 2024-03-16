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
        
    }

    // Update is called once per frame
    void Update()
    {
         Invoke("Prepared", growthTime);
    }
    
    private void Prepared()
    {
        _isRipe = true;
    }

    private void Harvest()
    {
        _isRipe = false;
    }
    
    
}
