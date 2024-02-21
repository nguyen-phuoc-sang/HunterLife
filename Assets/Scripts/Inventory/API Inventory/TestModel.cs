using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModel 
{
    public TestModel(string id, TestConModel itemName, int quantity)
    {
        _id = id;
        this.itemName = itemName;
        this.quantity = quantity;
    }

    public string _id {  get; set; }
    public TestConModel itemName { get; set; }
    public int quantity { get; set; }


    
}
