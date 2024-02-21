using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginResponseMoel 
{
    public LoginResponseMoel(bool status, string message, string id, int coin, string positionX, string positionY, string positionZ, int donemap,int[] chestmap)
    {
        this.status = status;
        this.message = message;
        this.id = id;
        this.coin = coin;
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;
        this.donemap = donemap;
        this.chestmap = chestmap;
    }

    public bool status { get; set; }
    public string message { get; set; }
    public string id { get; set; }
    public int coin { get; set; }
    public string positionX { get; set; }
    public string positionY { get; set; }
    public string positionZ { get; set; }
    public int donemap {get; set;}
    public int[] chestmap {get;set;}
}
