using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private enum VegetableType { Carrot, Corn, Eggplant, Pumpkin, Tomato, Turnip }
    private Dictionary<VegetableType, List<GameObject>> vegetableInventory = new Dictionary<VegetableType, List<GameObject>>();

    private void Awake()
    {
        foreach (VegetableType type in Enum.GetValues(typeof(VegetableType)))
        {
            vegetableInventory.Add(type, new List<GameObject>());
        }
    }

    public void AddVegetable(GameObject gameObject)
    {
        VegetableType type;
        if (Enum.TryParse(gameObject.tag, out type) && vegetableInventory.ContainsKey(type))
        {
            vegetableInventory[type].Add(gameObject);
            Debug.Log($"Added {gameObject.tag}");
        }
        else
        {
            Debug.LogError($"Invalid vegetable type: {gameObject.tag}");
        }
    }

    public bool UseVegetable(string typeString)
    {
        VegetableType type;
        if (Enum.TryParse(typeString, true, out type) && vegetableInventory[type].Count > 0)
        {
            vegetableInventory[type].RemoveAt(0);
            return true;
        }
        return false;
    }
}