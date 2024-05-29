using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VegetableController : MonoBehaviour
{
    public delegate void PlantReadyEventHandler(GameObject plant);

    public static event PlantReadyEventHandler OnPlantReady;

    public string type;
    public bool isRipe = false;
    public float growthTime;
    public bool isSeeded = false;
    private GameObject _soil;
    private bool _isEmpty;

    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

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
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetComponent<TimeBarPlant>().GetSlider().SetActive(false);

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player") && !isRipe)
        {
            transform.GetComponent<TimeBarPlant>().GetSlider().SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            transform.GetComponent<TimeBarPlant>().GetSlider().SetActive(false);
        }
    }

    public void SetSoil(GameObject o)
    {
        _soil = o;
    }
}