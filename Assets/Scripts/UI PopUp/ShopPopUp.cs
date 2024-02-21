using UnityEngine;

public class ShopPopUp : MonoBehaviour
{
    public GameObject miniMap, Setting, inventory, shopBuy, shopSell;

    public void ShowInventory()
    {
        inventory.SetActive(true);
        Setting.SetActive(false);
        miniMap.SetActive(false);

    }
    public void ShowMiniMap()
    {

        miniMap.SetActive(true);
        Setting.SetActive(false);

    }

    public void ShowSetting()
    {
        miniMap.SetActive(false);
        Setting.SetActive(true);

    }

    public void ShowShopBuy()
    {
        shopBuy.SetActive(true);
        shopSell.SetActive(false);
    }

    public void ShowShopSell()
    {
        shopSell.SetActive(true);
        //shopBuy.SetActive(false);

    }
}
