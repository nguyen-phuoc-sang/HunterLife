using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator Anidoor1, Anidoor2;
    public GameObject TreasureChest;
    //   public GameObject openShow;
    //  [SerializeField] private Transform viTri;

    [Header("Monter")]
    public GameObject[] quai = new GameObject[3];
    [Header("door")]
    public GameObject door1, door2, door;

    // Start is called before the first frame update
    void Start()
    {

        Anidoor1 = door1.GetComponent<Animator>();
        Anidoor2 = door2.GetComponent<Animator>();

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
            //  GameObject openShow2 = Instantiate(openShow, viTri.position, viTri.rotation);
            opendoors();
            a = -1;

        }
    }

    private void opendoors()
    {

        //    Anidoor1.PlayOneShot("door1");
        //     Anidoor2.PlayOneShot("door2");

        Anidoor1.SetTrigger("dong1");
        Anidoor2.SetTrigger("dong");


        Destroy(door, 0.5f);

        TreasureChest.SetActive(true);






    }

}
