using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject quaBoom;
    [SerializeField] private Transform viTri;

    private float timer;
    private float time = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DatBoom();
    }

    void DatBoom()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - timer > time)
        {
            GameObject datBoom = Instantiate(quaBoom, viTri.position, viTri.rotation);
            timer = Time.time;
        }
    }

}
