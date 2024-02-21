using Inventory;
using System.Collections;
using UnityEngine;

public class PlayerTeleportfarmXdungeon : MonoBehaviour
{
    private GameObject currentTeleporter;

    [Header("player")]
    public GameObject PlayerDungeon;
    public GameObject PlayerFarm;
    [Header("Canvan")]
    public GameObject CanvanDungeon;
    public GameObject Canvan;
    [Header("Camera")]
    public GameObject CameraDungeon;
    // public Camera CameraDungeonCanvas;
    public GameObject Camera;
    public GameObject pannettuto;

    private Item itemAPI;

    public float transitionTime = 1f;


    [SerializeField] private GameObject Loader;
    //[SerializeField] private GameObject Farm;
    //[SerializeField] private GameObject Road;
    //[SerializeField] private GameObject Town;
    //[SerializeField] private GameObject Dungeon;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentTeleporter != null)
            {
                LoadingTransition();
                // Vector3 originPosotion = cam.transform.position;

            }
        }



    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("teleporter"))
        {
            currentTeleporter = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("teleporter"))
        {
            int otherLayer = collision.gameObject.layer;

            if (otherLayer == LayerMask.NameToLayer("EnterDungeon"))
            {

                CameraDungeon.SetActive(true);
                Canvan.SetActive(false);
                Camera.SetActive(false);
                pannettuto.SetActive(true);

                //  Canvas canvas = GetComponent<Canvas>();
                //  canvas.worldCamera = CameraDungeonCanvas;

                transform.position += new Vector3(0, -2, 0);

                CanvanDungeon.SetActive(true);

                PlayerDungeon.SetActive(true);
                PlayerFarm.SetActive(false);

                //InventoryController inventoryController = FindObjectOfType<InventoryController>();
                //if (inventoryController != null)
                //{
                //    inventoryController.LoadItemSell();
                //}
                CreateScripts.createScriptsIntance.CreateMyScriptableObject();
                ShowPotion.playerLife.ShowPot();
                ShowPotion.playerLife.ShowKey();
                ShowPotion.playerLife.ShowMeat();
                //thay mini map

            }
            else if (otherLayer == LayerMask.NameToLayer("OutDungeon"))
            {
                pannettuto.SetActive(false);


                CanvanDungeon.SetActive(false);
                CameraDungeon.SetActive(false);
                transform.position += new Vector3(0, 2, 0);
                PlayerDungeon.SetActive(false);
                Canvan.SetActive(true);
                Camera.SetActive(true);
                PlayerFarm.SetActive(true);
                ShowPotion.playerLife.ShowPot();
                ShowPotion.playerLife.ShowKey();
                ShowPotion.playerLife.ShowMeat();
                //thay mini map

            }
        }
    }

    public void LoadingTransition()
    {
        StartCoroutine(Loading());
    }

    //Transition Area
    IEnumerator Loading()
    {
        Loader.SetActive(true);
        //play animation

        //teleport

        //wait
        yield return new WaitForSeconds(transitionTime);
        Loader.SetActive(false);

    }

}
