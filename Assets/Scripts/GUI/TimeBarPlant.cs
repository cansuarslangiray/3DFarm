using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeBarPlant : MonoBehaviour
{
    private float _timeBound = 30;
    private Slider _timeSlider;
    public GameObject planet;
    public Text timeText;

    public float passedTime = 0f;

    private void Start()
    {
        _timeBound = planet.GetComponent<VegetableController>().growthTime;
        _timeSlider = GameObject.Find("Slider").gameObject.GetComponent<Slider>();
        transform.position = new Vector3(planet.transform.position.x, planet.transform.position.y + 1.5f,
            transform.position.z);
        _timeSlider.value = 0;

    }

    private void Update()
    {
        if (passedTime < _timeBound)
        {
            passedTime += Time.deltaTime;

            _timeSlider.value = (passedTime / _timeBound);

            float remainingTime = _timeBound - passedTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);

            int seconds = Mathf.FloorToInt(remainingTime % 60);

             timeText.text= string.Format("{0:00}:{1:00}", minutes, seconds);
            
            

        }
    }
}
