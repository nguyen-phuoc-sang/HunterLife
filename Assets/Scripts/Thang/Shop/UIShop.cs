using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop.UI
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] UIShopItem itemPrefabs;

        [SerializeField] RectTransform contenPanel;

        [SerializeField]
        private UIShopDescription itemDescription;

        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIShopItem> ListOfUIItem = new List<UIShopItem>();

        public event Action<int> OnDescipttionRequested, OnItemActionRequested;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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
                UIShopItem uiItem = Instantiate(itemPrefabs, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contenPanel);
                ListOfUIItem.Add(uiItem);
                uiItem.OnItemclick += HandleItemSelection;
                uiItem.OnRightMouseButtonClick += HandleShowItemAction;

            }
        }
        internal void UpdateDesciption(int itemindex, Sprite iteamImage, string name, string description, int price)
        {
            itemDescription.SetDescription(iteamImage, name, description, price);
            DeselectAllItems();
            ListOfUIItem[itemindex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int priceItem)
        {
            if (ListOfUIItem.Count > itemIndex)
            {
                ListOfUIItem[itemIndex].SetData(itemImage, priceItem);
            }
        }

        private void HandleShowItemAction(UIShopItem shopItemUI)
        {
        }

        private void HandleItemSelection(UIShopItem shopItemUI)
        {
            int index = ListOfUIItem.IndexOf(shopItemUI);
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
            foreach (UIShopItem item in ListOfUIItem)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
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
