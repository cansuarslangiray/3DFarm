using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactionUI;
    private GameObject currentVillager;
    private Inventory playerInventory;
    public float interactionDistance = 1f;
    private GameObject[] _villagers;

    private VillagerRequest villagerRequest;

    void Start()
    {
        _villagers = GameObject.FindGameObjectsWithTag("Villager");
        playerInventory = GetComponent<Inventory>();
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (currentVillager != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactionUI.GetComponent<VillagerUI>().DisplayRequest(currentVillager);
                interactionUI.SetActive(true);
            }
        }
        else
        {
            interactionUI.SetActive(false);
        }
    }

    public void Give()
    {
        villagerRequest = currentVillager.GetComponent<VillagerRequest>();
        if (playerInventory.CanFulfillRequest(villagerRequest.currentRequests))
        {
            playerInventory.FulfillRequest(villagerRequest.currentRequests);
            Destroy(currentVillager);
            _villagers = GameObject.FindGameObjectsWithTag("Villager");
            currentVillager = null;
            interactionUI.SetActive(false);
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager") && _villagers.Contains(other.gameObject))
        {
            currentVillager = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Villager") && _villagers.Contains(other.gameObject))
        {
            currentVillager.GetComponent<Villager>().ResumeMovement();
            currentVillager = null;
            interactionUI.SetActive(false);
        }
    }
}