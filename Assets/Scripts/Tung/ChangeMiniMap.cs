using UnityEngine;

public class ChangeMiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject miniMap1, miniMap2, miniMap3, miniMap4;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map4toMap1"))
        {
            chooseMiniMap1();
        }
        else if (collision.gameObject.CompareTag("Map1toMap4Fix"))
        {
            chooseMiniMap4();
        }
        else if (collision.gameObject.CompareTag("Map1toMap2"))
        {
            chooseMiniMap2();
        }
        else if (collision.gameObject.CompareTag("Map2toMap1"))
        {
            chooseMiniMap1();
        }
        else if (collision.gameObject.CompareTag("Map2toMap3"))
        {
            chooseMiniMap3();
        }
        else if (collision.gameObject.CompareTag("Map3toMap2"))
        {
            chooseMiniMap2();
        }

    }

    void chooseMiniMap1()
    {
        miniMap1.SetActive(true);
        miniMap2.SetActive(false);
        miniMap3.SetActive(false);
        miniMap4.SetActive(false);
    }
    void chooseMiniMap2()
    {
        miniMap1.SetActive(false);
        miniMap2.SetActive(true);
        miniMap3.SetActive(false);
        miniMap4.SetActive(false);
    }
    void chooseMiniMap3()
    {
        miniMap1.SetActive(false);
        miniMap2.SetActive(false);
        miniMap3.SetActive(true);
        miniMap4.SetActive(false);
    }
    void chooseMiniMap4()
    {
        miniMap1.SetActive(false);
        miniMap2.SetActive(false);
        miniMap3.SetActive(false);
        miniMap4.SetActive(true);
    }
}
