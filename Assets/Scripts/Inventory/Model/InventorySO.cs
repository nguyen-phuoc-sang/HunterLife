using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Pathfinding.RaycastModifier;
using static UnityEditor.Progress;

namespace Inventory.Model
{

    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [field: SerializeField]
        private List<InventoryItem> inventoryItems;
        private List<InventoryAPI> inventoryItemsAPI;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        //public event Action<Dictionary<int, InventoryAPI>> OnInventoryUpdatedAPI;

        public Login login { get; private set; }
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmtyItem());
            }
        }
        public void AddItemSO(ItemSO itemSO)
        {
            // Tạo một InventoryItem mới từ ItemSO và thêm vào danh sách
            InventoryItem newItem = new InventoryItem
            {
                quantity = 1,
                itemSO = itemSO
            };
            inventoryItems.Add(newItem);

            // Gọi sự kiện thông báo rằng dữ liệu đã được cập nhật
            //  OnInventoryUpdated?.Invoke();
        }






        public void InitializeAPI()
        {
            inventoryItemsAPI = new List<InventoryAPI>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItemsAPI.Add(InventoryAPI.GetEmtyItem());
            }
        }


        // ****
        public int AddItem(ItemSO itemSO, int quantity)
        {

            if (itemSO.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {

                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(itemSO, quantity);
                    }
                    InformAboutChange();
                    return quantity;

                }
            }
            quantity = AddStackAbleItem(itemSO, quantity);
            InformAboutChange();
            return quantity;
        }

        public int AddItemShop(ItemSO itemSO, int quantity)
        {

            if (itemSO.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {

                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(itemSO, quantity);
                    }
                    InformAboutChange();
                    return quantity;

                }
            }
            quantity = AddStackAbleItemShop(itemSO, quantity);
            InformAboutChange();
            return quantity;
        }
          
        private int AddItemToFirstFreeSlot(ItemSO itemSO, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                itemSO = itemSO,
                quantity = quantity
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.IsEmty).Any() == false;

        private int AddStackAbleItem(ItemSO itemSO, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmty)
                    continue;
                if (inventoryItems[i].itemSO.idName == itemSO.idName)
                {
                    int amountPossibleTotake =
                        inventoryItems[i].itemSO.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleTotake)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].itemSO.MaxStackSize);
                        quantity -= amountPossibleTotake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, itemSO.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(itemSO, newQuantity);
            }
            return quantity;
        }

        private int AddStackAbleItemShop(ItemSO itemSO, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmty)
                    continue;
                if (inventoryItems[i].itemSO.id == itemSO.id)
                {
                    int amountPossibleTotake =
                        inventoryItems[i].itemSO.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleTotake)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].itemSO.MaxStackSize);
                        quantity -= amountPossibleTotake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, itemSO.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(itemSO, newQuantity);
            }
            return quantity;
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.itemSO, item.quantity);
            //  Debug.Log(item.itemSO.Name);
        }

        public void AddItemShop(InventoryItem item)
        {
            AddItemShop(item.itemSO, item.quantity);
            //  Debug.Log(item.itemSO.Name);
        }

        //new
        public void AddItemAPI(ItemSO item, int quantity)
        {
            AddItem(item, quantity);

        }


        // xóa vật phẩm
        public void RemoveItem(string idName, int quantityItem)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if ( inventoryItems[i].itemSO != null)
                {
                    if (inventoryItems[i].itemSO.idName == idName)
                    {
                        if (inventoryItems[i].quantity > 1)
                        {
                            inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity - quantityItem);
                            ItemAPILogin.itemAPI.DeleteItemInventoryNOLOAD(idName, 1);
                            InventoryController inventoryController = FindObjectOfType<InventoryController>();
                            if (inventoryController != null)
                            {
                                inventoryController.initalItems.Clear();
                                inventoryController.inventoryData.OnInventoryUpdated += inventoryController.UpdateInventoryUI;
                                inventoryController.LoadItemSell();
                            }
                        }
                        else
                        {
                            inventoryItems.Remove(inventoryItems[i]);
                            ItemAPILogin.itemAPI.DeleteItemInventory(idName, 1); 
                        }
                        
                       
                    }

                }

            }

        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemindex)
        {
            return inventoryItems[itemindex];

        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();


        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }


    }



    [System.Serializable]

    //new
    public struct InventoryAPI
    {
        public int quantityAPI;
        public int priceAPI;
        public ItemSOAPI itemSOAPI;


        public bool IsEmty => itemSOAPI == null;



        public static InventoryAPI GetEmtyItem() => new InventoryAPI
        {
            itemSOAPI = null,
            quantityAPI = 0,
            priceAPI = 0,
        };



    }


    [System.Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public int price;
        public ItemSO itemSO;

        public bool IsEmty => itemSO == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                itemSO = this.itemSO,
                quantity = newQuantity
            };
        }

        public static InventoryItem GetEmtyItem() => new InventoryItem
        {
            itemSO = null,
            quantity = 0

        };



        /* // Hàm tạo để thêm ScriptableObject tự động
         public InventoryItem(int newQuantity, ItemSO newItemSO)
         {
             quantity = newQuantity;
             itemSO = newItemSO;

         }
    */
        /*public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem(newQuantity, itemSO, scriptableObject);
        }

        public static InventoryItem GetEmtyItem() => new InventoryItem(0, null, null);*/

    }
}















