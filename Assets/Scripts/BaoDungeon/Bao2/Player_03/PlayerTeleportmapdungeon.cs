using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleportmapdungeon : MonoBehaviour
{
    private GameObject currentTeleporter;
    private GameObject transition;

    public GameObject panelBoss;
    private Animator bossss;



    // public Animator aniTransition;
    public float transitionTime = 0.5f;

    public GameObject cam;
    public GameObject pannettuto;

    private Vector3 Map, map1, maptuto, map2, map3, map4, map5, map5A, map6, map7, map8, map9, map10, map3A;// =  new Vector3(x, y, -9);
                                                                                                            //  [Header("Item")]
    public GameObject itemman1, itemmantuto, itemman2, itemman3, itemman4, itemman5, itemman3A, itemman6, itemman5A, itemman7, itemman8;
    //  [Header("Monter")]
    public GameObject MonterMan1, MonterMan2, MonterMan3, MonterMan4, MonterMan5, MonterMan6, MonterMan7;

    // UI ánh xạ mini map theo từng màn
    public RawImage miniMap;
    public Texture texture1, texturetuto, texture2, texture3, texture3a, texture4, texture5, texture5a, texture6, texture7, textureboss;

    [SerializeField] private GameObject Loader;
    public string key_Man = "man_save";
    //
    //
    //
    //  [Header("player")]
    // public GameObject PlayerDungeon;
    // public GameObject PlayerFarm;
    // [Header("Canvan")]
    // public GameObject CanvanDungeon;
    // public GameObject Canvan;
    //  [Header("Camera")]
    // public GameObject CameraDungeon;
    // public GameObject Camera;


    // Start is called before the first frame update
    void Start()
    {
        Map = new Vector3(184.5f, 81.5f, -9f);
        map1 = new Vector3(184.5f, 81.5f, -9f);
        maptuto = new Vector3(184.5f, 64.36f, -9f);
        map2 = new Vector3(209f, 81.5f, -9f);
        map3 = new Vector3(233f, 81.5f, -9);
        map3A = new Vector3(258f, 81.5f, -9);
        map4 = new Vector3(233f, 64.36f, -9);
        map5 = new Vector3(233f, 50.5f, -9);
        map5A = new Vector3(232f, 34.5f, -9);
        map6 = new Vector3(260f, 50.5f, -9);
        map7 = new Vector3(288, 50.5f, -9);
        map8 = new Vector3(312, 50.5f, -9);

        bossss = panelBoss.GetComponent<Animator>();

    }



    // Update is called once per frame
    void Update()


    {
        if (Login.loginResponse != null)
        {
            int CurrentMappp = Login.loginResponse.donemap;
            if (CurrentMappp >= 0)
            {
                Destroy(MonterMan1);
            }
            if (CurrentMappp >= 1)
            {
                Destroy(MonterMan2);
            }
            if (CurrentMappp >= 2)
            {
                Destroy(MonterMan3);
            }
            if (CurrentMappp >= 3)
            {
                Destroy(MonterMan4);
            }
            if (CurrentMappp >= 4)
            {
                Destroy(MonterMan5);
            }
            if (CurrentMappp >= 5)
            {
                Destroy(MonterMan6);
            }
            if (CurrentMappp >= 6)
            {
                Destroy(MonterMan7);
            }
            if (CurrentMappp >= 7)
            {
                // Destroy(itemman8);
            }
        }




        if (currentTeleporter != null)
        {
            LoadingTransition();
            // Vector3 originPosotion = cam.transform.position;
            changeMap(Map);
            // Debug.Log("VỊ trí x" + x);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sidemantuto"))
        {
            Map = map1;

            itemmantuto.SetActive(false);
            itemman1.SetActive(true);
            pannettuto.SetActive(false);

            //thay mini map
            miniMap.texture = texture1;


        }
        if (collision.gameObject.CompareTag("Sideman1"))
        {
            Map = maptuto;

            itemmantuto.SetActive(true);
            itemman1.SetActive(false);
            pannettuto.SetActive(true);

            //thay mini map
            miniMap.texture = texturetuto;


        }
        if (collision.gameObject.CompareTag("Sideman5.2"))
        {
            Map = map5A;
            itemman5.SetActive(false);
            itemman5A.SetActive(true);
            //thay mini map
            miniMap.texture = texture5a;


        }
        if (collision.gameObject.CompareTag("Sideman5A"))
        {
            Map = map5;
            itemman5.SetActive(true);
            itemman5A.SetActive(false);
            //thay mini map
            miniMap.texture = texture5;


        }
        if (collision.gameObject.CompareTag("Endman3"))
        {
            Map = map3A;

            itemman3A.SetActive(true);
            itemman3.SetActive(false);

            //thay mini map
            miniMap.texture = texture3a;


        }
        if (collision.gameObject.CompareTag("Startman3A"))
        {
            Map = map3;

            itemman3A.SetActive(false);
            itemman3.SetActive(true);

            //thay mini map
            // Debug.Log("Quay về map 3");
            miniMap.texture = texture3;


        }
        if (collision.gameObject.CompareTag("teleporter"))
        {
            currentTeleporter = collision.gameObject;
            int CurrentMappp = PlayerPrefs.GetInt(key_Man, 0);
            // Lấy layer của đối tượng
            int otherLayer = collision.gameObject.layer;


            // Kiểm tra layer và thực hiện xử lý
            if (otherLayer == LayerMask.NameToLayer("endman1"))
            {
                Map = map2;

                itemman2.SetActive(true);
                itemman1.SetActive(false);


                //thay mini map
                miniMap.texture = texture2;

            }
            else if (otherLayer == LayerMask.NameToLayer("startman2"))
            {
                Map = map1;

                itemman2.SetActive(false);
                itemman1.SetActive(true);

                //thay mini map
                miniMap.texture = texture1;
            }
            else if (otherLayer == LayerMask.NameToLayer("Endman2"))
            {
                Map = map3;


                itemman3.SetActive(true);
                itemman2.SetActive(false);

                //thay mini map
                miniMap.texture = texture3;

            }
            else if (otherLayer == LayerMask.NameToLayer("Startman3"))
            {
                Map = map2;

                itemman3.SetActive(false);
                itemman2.SetActive(true);

                //thay mini map
                miniMap.texture = texture2;

            }
            else if (otherLayer == LayerMask.NameToLayer("Endman3"))
            {
                Map = map3A;

                itemman3A.SetActive(true);
                itemman3.SetActive(false);

                //thay mini map
                miniMap.texture = texture3a;
            }
            else if (otherLayer == LayerMask.NameToLayer("Startman3A"))
            {
                // không có layer này nên không dùng được
                Map = map3;

                itemman3A.SetActive(false);
                itemman3.SetActive(true);

                //thay mini map
                Debug.Log("Quay về map 3");
                miniMap.texture = texture3;
            }
            else if (otherLayer == LayerMask.NameToLayer("Sideman3"))
            {
                if (Map == map3A)
                {
                    Map = map3;
                    itemman3A.SetActive(false);
                    itemman3.SetActive(true);
                    //thay mini map
                    Debug.Log("Quay về map 3");
                    miniMap.texture = texture3;
                }
                else
                {
                    Map = map4;

                    itemman4.SetActive(true);
                    itemman3.SetActive(false);

                    //thay mini map
                    miniMap.texture = texture4;
                }
            }
            else if (otherLayer == LayerMask.NameToLayer("Sideman4.1"))
            {

                Map = map3;

                itemman4.SetActive(false);
                itemman3.SetActive(true);

                //thay mini map
                miniMap.texture = texture3;
            }
            else if (otherLayer == LayerMask.NameToLayer("Sideman4.2"))
            {
                Map = map5;

                itemman5.SetActive(true);
                itemman4.SetActive(false);


                //thay mini map
                miniMap.texture = texture5;
            }
            else if (otherLayer == LayerMask.NameToLayer("Sideman5.1"))
            {
                Map = map4;

                itemman5.SetActive(false);
                itemman4.SetActive(true);

                //thay mini map
                miniMap.texture = texture4;
            }
            else if (otherLayer == LayerMask.NameToLayer("Sideman5.2"))
            {
                Map = map5A;
                itemman5.SetActive(false);
                itemman5A.SetActive(true);
                //thay mini map
                miniMap.texture = texture5a;
            }
            else if (otherLayer == LayerMask.NameToLayer("Sideman5A"))
            {
                Map = map5;
                itemman5.SetActive(true);
                itemman5A.SetActive(false);
                //thay mini map
                miniMap.texture = texture5;
            }
            else if (otherLayer == LayerMask.NameToLayer("Endman5"))
            {
                Map = map6;


                itemman6.SetActive(true);
                itemman5.SetActive(false);
                //thay mini map
                miniMap.texture = texture6;
            }
            else if (otherLayer == LayerMask.NameToLayer("Startman6"))
            {
                Map = map5;
                itemman5.SetActive(true);
                itemman6.SetActive(false);
                //thay mini map
                miniMap.texture = texture5;
            }
            else if (otherLayer == LayerMask.NameToLayer("Endman6"))
            {
                Map = map7;

                itemman7.SetActive(true);
                itemman6.SetActive(false);

                //thay mini map
                miniMap.texture = texture7;
            }
            else if (otherLayer == LayerMask.NameToLayer("Startman7"))
            {
                Map = map6;
                itemman6.SetActive(true);
                itemman7.SetActive(false);
                //thay mini map
                miniMap.texture = texture6;
            }
            else if (otherLayer == LayerMask.NameToLayer("Endman7"))
            {
                Map = map8;
                AudioManager.instance.PlaySfx("Boss");
                itemman8.SetActive(true);
                bossss.SetTrigger("Play");

                //thay mini map
                miniMap.texture = textureboss;
            }
            else if (otherLayer == LayerMask.NameToLayer("Startman8"))
            {
                Map = map7;

                itemman7.SetActive(true);
                itemman8.SetActive(false);
                //thay mini map
                miniMap.texture = texture7;
            }
            //  else if (otherLayer == LayerMask.NameToLayer("OutDungeon"))
            // {
            //     Map = map1;
            //    // itemman1.SetActive(false);
            //     CanvanDungeon.SetActive(false);
            //     CameraDungeon.SetActive(false);
            //     PlayerDungeon.SetActive(false);
            //     Canvan.SetActive(true);
            //     Camera.SetActive(true);
            //     PlayerFarm.SetActive(true);

            //     //thay mini map

            // }



        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("teleporter"))
        {
            currentTeleporter = null;
        }
        if (collision.gameObject.CompareTag("Sidemantuto"))
        {

            currentTeleporter = null;


        }
        if (collision.gameObject.CompareTag("Sideman1"))
        {

            currentTeleporter = null;


        }
    }

    public void LoadingTransition()
    {
        StartCoroutine(Loading());
    }
    public void changeMap(Vector3 map)
    {
        cam.transform.position = map;
    }


    //Transition Area
    IEnumerator Loading()
    {
        Loader.SetActive(true);
        //play animation
        //  aniTransition.SetTrigger("start");
        //teleport
        transform.position = currentTeleporter.GetComponent<TeleporterDungeon>().GetDestination().position;
        //wait
        yield return new WaitForSeconds(transitionTime);
        Loader.SetActive(false);

    }



}
