using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TimeBarPlant : MonoBehaviour
{
    private float _timeBound = 30;
    private GameObject _timeSlider;
    private GameObject _timeBarContainer;
    public GameObject timeSliderPrefab;

    private TMP_Text _timeText;

    public float passedTime = 0f;

    private void Start()
    {
        _timeBarContainer = GameObject.Find("TimeBarContainer");
        _timeSlider = Instantiate(timeSliderPrefab, SetPosition(), Quaternion.identity);
        _timeSlider.transform.SetParent(_timeBarContainer.transform, false);
        _timeBound = gameObject.GetComponent<VegetableController>().growthTime;
        _timeSlider.GetComponentInChildren<Slider>().value = 0;
        _timeText = _timeSlider.transform.GetComponentInChildren<TMP_Text>();
        _timeSlider.SetActive(true);
    }

    private void Update()
    {
        if (passedTime < _timeBound)
        {
            passedTime += Time.deltaTime;
            _timeSlider.GetComponentInChildren<Slider>().value = (passedTime / _timeBound);

            float remainingTime = _timeBound - passedTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        
        _timeSlider.transform.position = SetPosition();
    }

    private Vector3 SetPosition()
    {
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        return screenPosition;
    }

    public GameObject GetSlider()
    {
        return _timeSlider;
    }
}
