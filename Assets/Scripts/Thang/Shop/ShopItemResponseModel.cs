using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemResponseModel 
{
    public ShopItemResponseModel(string id, string shopName, ShopItemNameModel itemName, int price, int quantity)
    {
        _id = id;
        this.shopName = shopName;
        this.itemName = itemName;
        this.price = price;
        this.quantity = quantity;
    }

    public string _id { get; set; }
    public string shopName { get; set; }
    public ShopItemNameModel itemName { get; set; }
    public int price { get; set; }
    public int quantity { get; set; }
}
