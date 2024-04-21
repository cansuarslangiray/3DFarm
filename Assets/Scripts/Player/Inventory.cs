using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int sizeVegetable = 1;

    public List<GameObject> carrots = new List<GameObject>();
    public List<GameObject> corns = new List<GameObject>();
    public List<GameObject> eggplants = new List<GameObject>();
    public List<GameObject> pumpkins = new List<GameObject>();
    public List<GameObject> tomatoes = new List<GameObject>();
    public List<GameObject> turnips = new List<GameObject>();

    public void AddVegetable(GameObject gameObject)
    {
        Debug.Log(gameObject.gameObject.tag);
        gameObject.GetComponent<VegetableController>().isRipe = false;
        switch (gameObject.gameObject.tag)
        {
            case "Carrot":
                carrots.Add(Instantiate(gameObject));
                carrots.Add(Instantiate(gameObject));
                Destroy(gameObject);
                break;
            case "Corn":
                corns.Add(Instantiate(gameObject));
                corns.Add(Instantiate(gameObject));
                Destroy(gameObject);

                break;
            case "Eggplant":
                eggplants.Add(Instantiate(gameObject));
                eggplants.Add(Instantiate(gameObject));

                Destroy(gameObject);

                break;
            case "Pumpkin":
                pumpkins.Add(Instantiate(gameObject));
                pumpkins.Add(Instantiate(gameObject));
                Destroy(gameObject);

                break;
            case "Tamato":
                tomatoes.Add(Instantiate(gameObject));
                tomatoes.Add(Instantiate(gameObject));
                Destroy(gameObject);

                break;
            case "Turnip":
                turnips.Add(Instantiate(gameObject));
                turnips.Add(Instantiate(gameObject));
                Destroy(gameObject);
                break;
        }
    }

    public bool UseVegetable(string type)
    {
        switch (type)
        {
            case "Carrot":
                if (carrots.Count > 0)
                {
                    carrots.RemoveAt(0);
                    return true;
                }

                break;
            case "Corn":
                if (corns.Count > 0)
                {
                    corns.RemoveAt(0);
                    return true;
                }

                break;
            case "Eggplant":
                if (eggplants.Count > 0)
                {
                    eggplants.RemoveAt(0);
                    return true;
                }

                break;
            case "Pumpkin":
                if (pumpkins.Count > 0)
                {
                    pumpkins.RemoveAt(0);
                    return true;
                }

                break;
            case "Tamato":
                if (tomatoes.Count > 0)
                {
                    tomatoes.RemoveAt(0);
                    return true;
                }

                break;
            case "Turnip":
                if (turnips.Count > 0)
                {
                    turnips.RemoveAt(0);
                    return true;
                }

                break;
        }

        return false;
    }
}