using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemsInventoryModel
{
    public NewItemsInventoryModel(string userName, string itemName, int quantity)
    {
        this.userName = userName;
        this.itemName = itemName;
        this.quantity = quantity;
    }

   
    public string userName {  get; set; }
    public string itemName { get; set; }
    public int quantity { get; set; }
} 
    
   

