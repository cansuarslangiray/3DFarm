using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class InvGui : MonoBehaviour
{
    private TMP_Text _timeText;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _timeText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        var count = _player.GetComponent<Inventory>().CountEachVeg(name);
        _timeText.text = "x" + count;
    }

    public void SelectedVeg()
    {
        _player.GetComponent<PlayerMovement>().SetVeg(name);
        var count = _player.GetComponent<Inventory>().CountEachVeg(name) - 1;
        _timeText.text = "x" + count;
        _player.GetComponent<PlayerMovement>().Plant();
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}