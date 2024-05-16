using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvGui : MonoBehaviour
{
    private TMP_Text _timeText;
    public string name;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _timeText = GetComponentInChildren<TMP_Text>();
    }

    public void SelectedVeg()
    {
        _player.GetComponent<PlayerMovement>().SetVeg(name);
        var count = _player.GetComponent<Inventory>().CountEachVeg(name) - 1;
        _timeText.text = "x" + count;
        _player.GetComponent<PlayerMovement>().Plant();
    }
}