using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendOTPResponseMoel 
{
    public SendOTPResponseMoel(bool status, string message, string email)
    {
        this.status = status;
        this.message = message;
        this.email = email;
    }

    public bool status { get; set; }
    public string message { get; set; }
    public string email { get; set; }
}
