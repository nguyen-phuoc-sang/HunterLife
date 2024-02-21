using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterResponseMoel 
{
    public RegisterResponseMoel(bool status, string message, string id)
    {
        this.status = status;
        this.message = message;
        this.id = id;
    }

    public bool status { get; set; }
    public string message { get; set; }
    public string id { get; set; }
}
