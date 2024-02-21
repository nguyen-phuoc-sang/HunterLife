using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentOjb : MonoBehaviour
{
    [SerializeField] private GameObject transparentObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Color color = transparentObj.GetComponent<Renderer>().material.color;
            color.a = 0.5f;
            transparentObj.GetComponent<Renderer>().material.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            Color color = transparentObj.GetComponent<Renderer>().material.color;
            color.a = 1f;
            transparentObj.GetComponent<Renderer>().material.color = color;
    }
}
