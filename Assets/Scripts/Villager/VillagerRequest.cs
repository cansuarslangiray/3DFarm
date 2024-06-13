using UnityEngine;
using System.Collections.Generic;

public class VillagerRequest : MonoBehaviour
{
    public Dictionary<Inventory.VegetableType, int> currentRequests = new Dictionary<Inventory.VegetableType, int>();
    public Dictionary<Inventory.VegetableType, int> vegetablePrices = new Dictionary<Inventory.VegetableType, int>
    {
        { Inventory.VegetableType.Tomato, 5 },
        { Inventory.VegetableType.Carrot, 3 },
        { Inventory.VegetableType.Corn, 4 },
        { Inventory.VegetableType.Eggplant, 7 },
        { Inventory.VegetableType.Pumpkin, 8 },
        { Inventory.VegetableType.Turnip, 4 }
    };

    void Start()
    {
        GenerateNewRequest();
    }

    public void GenerateNewRequest()
    {
        currentRequests.Clear();
        int numberOfItems = Random.Range(1, 5); 
        List<int> usedIndices = new List<int>();
        Inventory.VegetableType[] vegetableTypes = (Inventory.VegetableType[])System.Enum.GetValues(typeof(Inventory.VegetableType));

        for (int i = 0; i < numberOfItems; i++)
        {
            int itemIndex;
            do
            {
                itemIndex = Random.Range(0, vegetableTypes.Length);
            } while (usedIndices.Contains(itemIndex));

            usedIndices.Add(itemIndex);
            Inventory.VegetableType requestItem = vegetableTypes[itemIndex];
            int itemCount = Random.Range(1, 11); 

            currentRequests.Add(requestItem, itemCount);
        }
    }

    public int CalculateTotalReward()
    {
        int totalReward = 0;
        foreach (var request in currentRequests)
        {
            if (vegetablePrices.ContainsKey(request.Key))
            {
                totalReward += request.Value * vegetablePrices[request.Key];
            }
        }
        return totalReward;
    }
}
