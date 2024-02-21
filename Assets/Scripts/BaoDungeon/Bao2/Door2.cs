using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{

    private Animator Anidoor1;
     public GameObject TreasureChest;
       public GameObject openShow;
        [SerializeField] private Transform viTri;
    [Header("Monter")]
    public GameObject[] quai = new GameObject[3];
    [Header("door")]
    public GameObject door1, door;

    // Start is called before the first frame update
    void Start()
    {

        Anidoor1 = door1.GetComponent<Animator>();
       

    }

    // Update is called once per frame
    void Update()
    {

        int a = quai.Length;
        for (int i = 0; i < quai.Length; i++)
        {
            if (quai[i] == null)
            {
                a = a - 1;

            }
        }
        if (a == 0)
        {
            opendoors();
            a = -1;
        }
    }

    private void opendoors()
    {

        //    Anidoor1.PlayOneShot("door1");
        //     Anidoor2.PlayOneShot("door2");
        Anidoor1.SetTrigger("mo");
      
        Destroy(door, 0.5f);
          TreasureChest.SetActive(true);


    }

}
