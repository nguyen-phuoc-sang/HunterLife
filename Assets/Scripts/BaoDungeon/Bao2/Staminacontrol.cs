using Inventory;
using System.Collections;
using UnityEngine;

public class Staminacontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thelucfull, theluc34, theluc12, theluc14, theluchet;
    public PlayerLife playerLife;
    public PlayerMovement playerMovement;
    int tongmeat2;
    public int currentStamina = 100;
    public int maxStamina = 100;
    public float decreaseInterval = 1f; // Thời gian giảm Stamina mỗi lần
    public int decreaseAmount = 5; // Lượng Stamina giảm mỗi lần
    void Start()
    {
        tongmeat2 = playerLife.tongmeat;
        currentStamina = maxStamina;
        StartCoroutine(DecreaseStaminaOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && ShowPotion.playerLife.Meat > 0)
        {
            IncreaseStamina(10);
            ShowPotion.playerLife.Meat -= 1;
            InventoryController inventoryController = FindObjectOfType<InventoryController>();
            if (inventoryController != null)
            {
                inventoryController.removeItem("657ee6149ffb7c266c17f886", 1);
            }

        }
        if (currentStamina >= 75 && currentStamina <= 100) { hienthelucfull(); }
        if (currentStamina >= 50 && currentStamina < 75) { hientheluc34(); }
        if (currentStamina >= 25 && currentStamina < 50) { hientheluc12(); }

        if (currentStamina > 0 && currentStamina < 25) { hientheluc14(); }
        if (currentStamina <= 0) { hientheluchet(); }

    }
    public void hienthelucfull()
    {
        thelucfull.SetActive(true);
        theluc34.SetActive(false);
        theluc12.SetActive(false);
        theluc14.SetActive(false);
        theluchet.SetActive(false);
        playerMovement.setSpeedRun(6);


    }
    public void hientheluc14()
    {
        thelucfull.SetActive(false);
        theluc34.SetActive(false);
        theluc12.SetActive(false);
        theluc14.SetActive(true);
        theluchet.SetActive(false);
        playerMovement.setSpeedRun(3);

    }
    public void hientheluc12()
    {
        thelucfull.SetActive(false);
        theluc34.SetActive(false);
        theluc12.SetActive(true);
        theluc14.SetActive(false);
        theluchet.SetActive(false);
        playerMovement.setSpeedRun(4);

    }
    public void hientheluc34()
    {
        thelucfull.SetActive(false);
        theluc34.SetActive(true);
        theluc12.SetActive(false);
        theluc14.SetActive(false);
        theluchet.SetActive(false);
        playerMovement.setSpeedRun(5);

    }
    public void hientheluchet()
    {
        thelucfull.SetActive(false);
        theluc34.SetActive(false);
        theluc12.SetActive(false);
        theluc14.SetActive(false);
        theluchet.SetActive(true);
        playerMovement.setSpeedRun(1);

    }
    IEnumerator DecreaseStaminaOverTime()
    {
        while (currentStamina > 0)
        {
            yield return new WaitForSeconds(decreaseInterval);
            DecreaseStamina(decreaseAmount);
        }
    }

    void DecreaseStamina(int amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0, maxStamina);
        //  Debug.Log("Current Stamina: " + currentStamina);
    }

    public void IncreaseStamina(int amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
        // Debug.Log("Stamina Increased! Current Stamina: " + currentStamina);
    }

    public int GetStamina()
    {
        return currentStamina;
    }
}
