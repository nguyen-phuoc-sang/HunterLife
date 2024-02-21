using Inventory.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private UIInventoryDescription itemDescription;

    [SerializeField]
    private RectTransform contenPanel;

    List<UIInventoryItem> listOfUIItem = new List<UIInventoryItem>();

    private void Awake()
    {
        Hide();
    }



    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDiscription();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
