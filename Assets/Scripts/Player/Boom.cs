using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator ani;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("BoomNo", 1f);
    }


    private void BoomNo()
    {
        if (isActive)
        {
            ani.SetBool("IsBoomNo", true);
            Destroy(gameObject,0.15f);
            Debug.Log("Chạm enemy");
        }  

        ani.SetBool("IsBoomNo", true);
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var name = collision.gameObject.tag;

        //khi enemies chạm vào
        if (collision.gameObject.CompareTag("enemy3"))
        {
            // nổ ngay
            isActive= true;
        }
    }

}
