using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPostion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Login.loginResponse != null)
        {
            if (Login.loginResponse.positionX != "")
            {
                float x = float.Parse(Login.loginResponse.positionX);
                float y = float.Parse(Login.loginResponse.positionY);
                transform.position = new Vector3(x, y, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
