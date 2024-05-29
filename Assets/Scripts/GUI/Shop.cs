using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject _player;
    public GameObject item;
    private TMP_Text _timeText;
    private bool _isOpen = false;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _timeText = GetComponentInChildren<TMP_Text>();
    }


    public void Shopping()
    {
        if (int.TryParse(_timeText.text.Substring(1), out int itemCount))
        {
            var playerMovement = _player.GetComponent<PlayerMovement>();

            if (playerMovement.GetMoney() > itemCount)
            {
                gameObject.SetActive(false);
                Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject newShopObject = Instantiate(item, spawnPosition, Quaternion.identity);
                newShopObject.AddComponent<DragAndDrop>();
            }
        }
    }

    public void ActivateShop()
    {
        if (!_isOpen)
        {
            gameObject.SetActive(true);
            _isOpen = true;
        }
        else
        {
            gameObject.SetActive(false);
            _isOpen = false;
        }
    }
}