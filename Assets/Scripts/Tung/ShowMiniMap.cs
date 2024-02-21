using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMiniMap : MonoBehaviour
{
    private bool isShow = true;
    public GameObject MiniMapArea;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void checkShow()
    {
        if (isShow)
        {
            MiniMapArea.SetActive(true);
        }
        else
        {
            MiniMapArea.SetActive(false);
        }
        isShow = !isShow;
    }
        
}
