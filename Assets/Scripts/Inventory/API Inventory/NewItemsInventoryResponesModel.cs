using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemsInventoryResponesModel
{
    public NewItemsInventoryResponesModel(bool status)
    {
        this.status = status;
    }

    public bool status { get; set; }
}
