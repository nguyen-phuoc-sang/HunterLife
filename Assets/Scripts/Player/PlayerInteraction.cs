using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject savePosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    // bắt sự kiện va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* if (collision.gameObject.CompareTag("SavePosition"))
         {
             savePosition.SetActive(true);
         }*/
    }
    
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        string Objectname = collision.attachedRigidbody.gameObject.name;
        if (collision.gameObject.CompareTag("wood"))
        {

            Destroy(GameObject.Find(Objectname));
            //  Debug.Log(">>>>>>>>>>.>>>>>>>>>>>>>>.");

        }


    }*/
}
