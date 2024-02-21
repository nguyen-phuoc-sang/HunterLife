using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChestModel 
{
    public SaveChestModel(string id, int index,int indexchestMap )
    {
        this.id = id;
        this.index = index;
        this.indexchestMap = indexchestMap;
    }
   public string id {get; set;}
   public int index {get; set;}
   public int indexchestMap {get; set;}
}