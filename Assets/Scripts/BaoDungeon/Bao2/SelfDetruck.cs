using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDetruck : MonoBehaviour
{

     public float DestroyTime;
     private float Timer;

    // Start is called before the first frame update
    void Start()
    {
     Timer = DestroyTime;   
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer<=0){
            Destroy(gameObject);
        }
    }
}
