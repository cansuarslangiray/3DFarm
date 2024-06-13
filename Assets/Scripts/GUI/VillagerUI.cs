using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class VillagerUI : MonoBehaviour
{
    public TMP_Text requestText;
    public TMP_Text total;

    public void DisplayRequest(GameObject o)
    {
        Dictionary<Inventory.VegetableType, int> requests = o.GetComponent<VillagerRequest>().currentRequests;
        requestText.text = "I need:\n";
        foreach (KeyValuePair<Inventory.VegetableType, int> request in requests)
        {
            requestText.text += $"{request.Value} x {request.Key}\n";
        }

        total.text = "Total: " + o.GetComponent<VillagerRequest>().CalculateTotalReward();
    }
}