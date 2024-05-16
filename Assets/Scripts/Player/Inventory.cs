using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum VegetableType
    {
        Carrot,
        Corn,
        Eggplant,
        Pumpkin,
        Tomato,
        Turnip
    }

    public GameObject[] prefabs;

    
    public Dictionary<VegetableType, List<GameObject>> vegetableInventory =
        new Dictionary<VegetableType, List<GameObject>>();

    
    public Dictionary<VegetableType, int> vegetableCounts = 
        new Dictionary<VegetableType, int>();

    private void Awake()
    {
        foreach (VegetableType type in Enum.GetValues(typeof(VegetableType)))
        {
            vegetableInventory.Add(type, new List<GameObject>());
            vegetableCounts.Add(type, 0); 
        }
    }

    public void AddVegetable(GameObject plant)
    {
        if (plant == null)
        {
            Debug.LogError("Invalid plant object.");
            return;
        }

        VegetableType type;
        if (Enum.TryParse(plant.GetComponent<VegetableController>().type, out type) &&
            vegetableInventory.ContainsKey(type))
        {
            vegetableInventory[type].Add(plant);
            vegetableCounts[type]++; 
            vegetableCounts[type]++; 

            Debug.Log($"Added {type} (1x). Total count for {type}: {vegetableCounts[type]}");
        }
        else
        {
            Debug.LogError($"Invalid vegetable type: {plant.GetComponent<VegetableController>().type}");
        }
    }

    public bool UseVegetable(string typeString)
    {
        VegetableType type;
        if (Enum.TryParse(typeString, true, out type) && vegetableInventory[type].Count > 0)
        {
            vegetableInventory[type].RemoveAt(0);
            vegetableCounts[type]--; 
            return true;
        }

        return false;
    }

    public GameObject GetPlantPrefab(VegetableType type)
    {
        if (vegetableInventory.ContainsKey(type) && vegetableInventory[type].Count > 0)
        {
            Debug.Log("HALOOO");
            return vegetableInventory[type][0];
        }
        return null;
    }

    public int CountEachVeg(string type)
    {
        VegetableType typeVeg;
        if (Enum.TryParse(type, true, out typeVeg))
        {
            return vegetableCounts[typeVeg]; 
        }

        return 0;
    }
}
