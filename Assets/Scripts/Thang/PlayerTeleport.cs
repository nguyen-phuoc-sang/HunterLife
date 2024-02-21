using System.Collections;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private GameObject transition;

    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;
    public Animator aniTransition;
    public float transitionTime = 1f;
    private int changeMap;

    [SerializeField] private GameObject Loader;
    //[SerializeField] private GameObject Farm;
    //[SerializeField] private GameObject Road;
    //[SerializeField] private GameObject Town;
    //[SerializeField] private GameObject Dungeon;


    // Start is called before the first frame update
    void Start()
    {
        changeMap = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTeleporter != null)
        {
            LoadingTransition();
            LoadSelectedMiniMap();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("teleporter"))
        {
            currentTeleporter = collision.gameObject;
            // Lấy layer của đối tượng
            int otherLayer = collision.gameObject.layer;

            // Kiểm tra layer và thực hiện xử lý
            if (otherLayer == LayerMask.NameToLayer("Map4-1"))
            {

                changeMap = 1;
            }
            else if (otherLayer == LayerMask.NameToLayer("Map1-4"))
            {

                changeMap = 4;
            }
            else if (otherLayer == LayerMask.NameToLayer("Map1-2"))
            {

                changeMap = 2;
            }
            else if (otherLayer == LayerMask.NameToLayer("Map2-1"))
            {

                changeMap = 1;
            }
            else if (otherLayer == LayerMask.NameToLayer("Map2-3"))
            {

                changeMap = 3;
            }
            else if (otherLayer == LayerMask.NameToLayer("Map3-2"))
            {

                changeMap = 2;
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

    public void LoadingTransition()
    {
        StartCoroutine(Loading());
    }

    //Transition Area
    IEnumerator Loading()
    {
        Loader.SetActive(true);
        //play animation
        aniTransition.SetTrigger("start");
        //teleport
        transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
        //wait
        yield return new WaitForSeconds(transitionTime);
        Loader.SetActive(false);

    }

    void LoadSelectedMiniMap()
    {
        switch (changeMap)
        {
            case 0:
                //  Debug.Log("Chưa tới vùng chuyển map");
                break;
            case 1:
                showMap1();
                break;
            case 2:
                showMap2();
                break;
            case 3:
                showMap3();
                break;
            case 4:
                showMap4();
                break;

        }
    }
    void showMap1()
    {
        map1.SetActive(true);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    void showMap2()
    {
        map1.SetActive(false);
        map2.SetActive(true);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    void showMap3()
    {
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(true);
        map4.SetActive(false);
    }
    void showMap4()
    {
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(true);
    }
}
