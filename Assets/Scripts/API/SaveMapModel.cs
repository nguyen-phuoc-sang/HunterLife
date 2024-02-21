using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMapModel 
{
    public SaveMapModel(string id, int donemap)
    {
        this.id = id;
        this.donemap = donemap;
    }
   public string id {get; set;}
   public int donemap {get; set; }
}
