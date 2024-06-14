using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    public PlayerMovement playerMovement; 

    private void Start()
    {
        UpdateMoneyDisplay();
    }

    public void UpdateMoneyDisplay()
    {
        moneyText.text = "Money: $" + playerMovement._money;
    }

    public void DeductMoney(int amount)
    {
        playerMovement._money -= amount;
        UpdateMoneyDisplay();
    }
}