using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public GameObject Door, TreasureChestOpen, TreasureChestClose, TreasureChestIT, dialogFull, dialogLifePot;
    public PlayerLife playerLife;
    public GameObject open;

    public int numKey;
    public GameObject Key;
    public int numLifePot;
    public GameObject LifePot;
    bool Opench = true;
    int B = 1;
    [SerializeField] private Transform viTri;
    [SerializeField] private Transform viTriKey;
    [SerializeField] private Transform viTriLifePot;
    [SerializeField] private Transform viTriPlayer;
    public string key_Chest = "ChestMan_1";
    public int Chestnum;

    // Start is called before the first frame update
    void Start()
    {
        int a;
        // B = PlayerPrefs.GetInt(key_Chest, 1);
        if (Login.loginResponse != null)
        {
            for (a = 0; a <= Login.loginResponse.chestmap.Length; a++)
            {
                if (a == Chestnum)
                {
                    B = Login.loginResponse.chestmap[a];
                    Debug.Log("///1" + B);
                    Debug.Log("///A" + Login.loginResponse.chestmap[a]);

                    if (B == 1)
                    {
                        Debug.Log("///2" + B);

                    }
                    if (B == 0)

                    {
                        Opench = false;
                        TreasureChestOpen.SetActive(true);
                        TreasureChestClose.SetActive(false);
                        Debug.Log("///3" + B);

                    }
                    return;

                }
            }


        }




    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, viTriPlayer.position);
        if (distance < 3f && Opench)
        {
            if (numKey == 0)
            {
                dialogLifePot.SetActive(true);

            }
            else
            {
                dialogFull.SetActive(true);
            }

        }
        else
        {
            dialogLifePot.SetActive(false);
            dialogFull.SetActive(false);


        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Opench && Door == null && playerLife.Key > 0 && collision.gameObject.CompareTag("player"))
        {
            if (B == 1)
            {
                Open();
            }

        }
    }
    public void Open()
    {
        B = 0;
        ItemAPILogin.itemAPI.SaveChest(Chestnum, B);

        //playerLife.TongKey(-1);
        ShowPotion.playerLife.Key -= 1;
        ItemAPILogin.itemAPI.DeleteItemInventory("6574bc92db53a20b56ab4326", 1);
        Opench = false;
        TreasureChestOpen.SetActive(true);
        AudioManager.instance.PlaySfx("MoRuong");
        TreasureChestClose.SetActive(false);
        GameObject Open2 = Instantiate(open, viTri.position, viTri.rotation);
        for (int i = 0; i < numKey; i++)
        {

            viTriKey.position += new Vector3(0, -i * 0.6f, 0);
            GameObject Key2 = Instantiate(Key, viTriKey.position, viTriKey.rotation);
        }
        for (int i = 0; i < numLifePot; i++)
        {
            viTriLifePot.position += new Vector3(0, -i * 0.6f, 0);
            GameObject LifePot2 = Instantiate(LifePot, viTriLifePot.position, viTriLifePot.rotation);
        }
        Debug.Log("///");



    }
}
