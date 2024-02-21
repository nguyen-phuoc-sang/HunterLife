using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] UIInventoryItem itemPrefabs;

        [SerializeField] RectTransform contenPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIInventoryItem> ListOfUIItem = new List<UIInventoryItem>();

        /* public Sprite image, image2;
         public int quantity;
         public string title,description;*/

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescipttionRequested, OnItemActionRequested,
            OnStartDragging;

        public event Action<int, int> OnSwapItem;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDiscription();
        }

        public void InitInventory(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefabs, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contenPanel);
                ListOfUIItem.Add(uiItem);
                uiItem.OnItemclick += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseButtonClick += HandleShowItemAction;

            }
        }
        internal void UpdateDesciption(int itemindex, Sprite iteamImage, string name, string description)
        {
            itemDescription.SetDescription(iteamImage, name, description);
            DeselectAllItems();
            ListOfUIItem[itemindex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (ListOfUIItem.Count > itemIndex)
            {
                ListOfUIItem[itemIndex].SetData(itemImage, itemQuantity);
            }
        }


        private void HandleShowItemAction(UIInventoryItem inventoryItemUI)
        {
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {

            ResetDraggtedItem();

        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = ListOfUIItem.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItem?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggtedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = ListOfUIItem.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);




            Debug.Log("Dang keo >>>>>>");
        }

        public void CreateDraggItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = ListOfUIItem.IndexOf(inventoryItemUI);
            if (index == -1) return;
            OnDescipttionRequested?.Invoke(index);

        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();

        }

        public void ResetSelection()
        {
            itemDescription.ResetDiscription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in ListOfUIItem)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggtedItem();
        }

        internal void ResetAllItem()
        {
            foreach (var item in ListOfUIItem)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateData(int key, Coroutine coroutine, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}