using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using Sell.UI;
using System;

public class SellController : MonoBehaviour
{
    [SerializeField]
    private UIInventorySell inventoryUI;

    [SerializeField]
    public InventorySO inventoryData;
    public SellController inventoryController;

    public ItemSO newScriptableObjectt;

    public List<InventoryItem> initalItems = new List<InventoryItem>();

    public Login login { get; private set; }
    public ItemSO itemSO;
    public static int quantity;
    public static int price;
    public static string idName;
    // Start is called before the first frame update
    void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (var item in inventoryData.GetCurrentInventoryState())
        {

            inventoryUI.UpdateData(item.Key,
              item.Value.itemSO.IteamImage,
              item.Value.quantity);

        }
    }

    public void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;


        // Kiểm tra và thêm newScriptableObject vào danh sách
        if (newScriptableObjectt != null)
        {
            // Tạo một InventoryItem mới và thêm vào danh sách
            InventoryItem newItem = new InventoryItem
            {
                itemSO = newScriptableObjectt,
                quantity = newScriptableObjectt.quantity, // Số lượng bạn muốn thêm vào danh sách
            };

            initalItems.Add(newItem);
        }

        foreach (InventoryItem item in initalItems)
        {
            if (item.IsEmty)
                continue;
            inventoryData.AddItem(item);
        }

    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItem();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.itemSO.IteamImage, item.Value.quantity);
        }
    }
    private void UpdateInventoryUIAPI(Dictionary<int, InventoryAPI> inventoryStatee)
    {
        inventoryUI.ResetAllItem();
        foreach (var item in inventoryStatee)
        {
            inventoryUI.UpdateData(item.Key, item.Value.itemSOAPI.IteamImageAPI, item.Value.quantityAPI);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitInventory(inventoryData.Size);
        inventoryUI.OnDescipttionRequested += HandleDesciptionRequets;
        inventoryUI.OnItemActionRequested += HandleItemActionRequets;
    }

    private void HandleItemActionRequets(int itemindex)
    {
        throw new NotImplementedException();
    }

    private void HandleDesciptionRequets(int itemindex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemindex);
        if (inventoryItem.IsEmty)
        {
            inventoryUI.ResetSelection();
            return;

        }

        ItemSO item = inventoryItem.itemSO;
        //  TestModel model;
        quantity = item.quantity;
        price = item.price;
        idName = item.idName;
        UISellDescription.currentTotal = 1;
        inventoryUI.UpdateDesciption(itemindex, item.IteamImage, item.name, item.Description, item.price, 1);





    }
}
