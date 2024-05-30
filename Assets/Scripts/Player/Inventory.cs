using UnityEngine;
using System;
using System.Collections.Generic;


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

    public Dictionary<VegetableType, int> vegetableCounts =
        new Dictionary<VegetableType, int>();

    private void Awake()
    {
        foreach (VegetableType type in Enum.GetValues(typeof(VegetableType)))
        {
            vegetableCounts.Add(type, 0);
        }
    }

    public void AddVegetable(GameObject plant)
    {
        if (plant == null)
        {
            return;
        }

        VegetableController controller = plant.GetComponent<VegetableController>();
        if (controller == null)
        {
            return;
        }

        VegetableType type;
        if (Enum.TryParse(controller.type, out type) && vegetableCounts.ContainsKey(type))
        {
            vegetableCounts[type]++;
            
        }
       
    }

    public GameObject GetPlantPrefab(VegetableType type)
    {
        foreach (GameObject prefab in prefabs)
        {
            VegetableController controller = prefab.GetComponent<VegetableController>();
            if (controller != null && Enum.TryParse(controller.type, out VegetableType prefabType) &&
                prefabType == type)
            {
                return prefab;
            }
        }

        return null;
    }

    public bool UseVegetable(string type)
    {
        if (Enum.TryParse(type, true, out VegetableType vegetableType))
        {
            if (vegetableCounts.ContainsKey(vegetableType) && vegetableCounts[vegetableType] > 0)
            {
                vegetableCounts[vegetableType]--;
                GameObject plantPrefab = GetPlantPrefab(vegetableType);
                if (plantPrefab != null)
                {
                    Instantiate(plantPrefab);
                }

                return true;
            }
        }


        return false;
    }

    public int CountEachVeg(string type)
    {
        if (Enum.TryParse(type, true, out VegetableType vegetableType))
        {
            if (vegetableCounts.ContainsKey(vegetableType))
            {
                return vegetableCounts[vegetableType];
            }
        }

        return 0;
    }
}