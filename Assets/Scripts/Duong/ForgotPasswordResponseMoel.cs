using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgotPasswordResponseMoel 
{
    public ForgotPasswordResponseMoel(bool status, string message)
    {
        this.status = status;
        this.message = message;
    }

    public bool status { get; set; }
    public string message { get; set; }
}
