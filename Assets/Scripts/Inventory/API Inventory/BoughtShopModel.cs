using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtShopModel 
{
    public BoughtShopModel(string id, string userName)
    {
        this.id = id;
        this.userName = userName;
    }

    public string id {  get; set; }
    public string userName { get; set; }
}
