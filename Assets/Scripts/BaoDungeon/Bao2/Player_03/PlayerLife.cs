using Inventory;
using System.Collections;
using TMPro;
using UnityEngine;



public class PlayerLife : MonoBehaviour
{
    private Animator ani;
    public GameObject heal = default;
    public Rigidbody2D rigidbody2D;
    [SerializeField] private Transform viTriheal;
    //public TextMeshProUGUI TextLifePot;
    public TextMeshProUGUI TextKey;
    //int LifePot = 2;
    public int Key;
    public int tongmeat = 2;


    public int CharLife = 10;
    public int CharLifeMax = 10;
    [SerializeField] private SimpleFlash flashEffect;


    // Start is called before the first frame update
    void Start()
    {

        ani = GetComponent<Animator>();
        //TextKey.text = "X " + Key;
        //TextLifePot.text = "X " + LifePot;

    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (ShowPotion.playerLife.LifePot > 0 && CharLife < CharLifeMax)
            {
                CharLife = CharLife + 1;
                //TongLifePot(-1);
                AudioManager.instance.PlaySfx("Heal");
                ani.Play("Char_Attack_LR");
                GameObject heal2 = Instantiate(heal, viTriheal.position, viTriheal.rotation);
                ShowPotion.playerLife.LifePot -= 1;
                InventoryController inventoryController = FindObjectOfType<InventoryController>();
                if (inventoryController != null)
                {
                    inventoryController.removeItem("651ff3786d1b88d6eb0d18e4", 1);
                }
            }

        }





        if (CharLife == 0)
        {

            //    ani.Play("Player_Die");
        }
        //         Heart1empty.SetActive(true);



    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Slimebite"))
        {
            matmau(1);
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            matmau(1);
        }
        if (collision.gameObject.CompareTag("BossBullet"))
        {
            matmau(1);
        }
        if (collision.gameObject.CompareTag("BossAttack"))
        {
            matmau(2);
        }


        if (collision.gameObject.CompareTag("rawmeat"))
        {

            Destroy(collision.gameObject);
            //TongLifePot(1);


        }
        if (collision.gameObject.CompareTag("meat"))
        {
            //Tongmeat(1);
            //Destroy(collision.gameObject);
            // TongKey(1);


        }
    }
    public void TongLifePot(int Pot)
    {
        ShowPotion.playerLife.LifePot += Pot;
        //TextLifePot.text = "X " + LifePot;
    }
    public void Tongmeat(int meat)
    {
        tongmeat += meat;
        //TextLifePot.text = "X " + LifePot;
    }
    public void TongKey(int K)
    {
        Key += K;
        TextKey.text = "X " + Key;
    }

    public void matmau(int dame)
    {
        flashEffect.Flash();

        //    Instantiate(PopUpDame,originPosotion,Quaternion.identity
        //    );
        PlayHitSound();
        StartCoroutine(ShakeOnce(0.16f, 0.012f));
        CharLife = CharLife - dame;

    }
    public IEnumerator ShakeOnce(float time, float magnitude)
    {
        Vector3 originPosotion = transform.position;
        float totalTime = 0f;

        while (totalTime < time)
        {
            totalTime += Time.deltaTime;
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;
            float z = Random.Range(-1, 1) * magnitude;

            transform.position += new Vector3(x, y, z);

            yield return null;
        }

        transform.position = originPosotion;


    }

    public void PlayHitSound()
    {
        AudioManager.instance.PlaySfx("Hit");
    }



}

