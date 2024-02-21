using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindObject : MonoBehaviour
{
    [SerializeField] private Renderer test;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Color color = test.GetComponent<Renderer>().material.color;
            color.a = 0.5f;
            test.GetComponent<Renderer>().material.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Color color = test.GetComponent<Renderer>().material.color;
        color.a = 1f;
        test.GetComponent<Renderer>().material.color = color;
    }

}
