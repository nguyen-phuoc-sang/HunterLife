using UnityEngine;

public class MapMN : MonoBehaviour
{
    public GameObject beginArea;
    public GameObject myFarm;
    public GameObject roadToMyFarm;
    public GameObject beach;
    public GameObject town;
    public GameObject dungeon;

    private void Start()
    {
        SetActiveObjects(false, myFarm, roadToMyFarm, beach, dungeon);
    }

    private void Update()
    {
        // Your update logic here
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SetActiveObjects(false, myFarm, roadToMyFarm, beach, town, dungeon);

        if (collision.gameObject.CompareTag("myFarm"))
        {
            Debug.Log("Va Cham");
            myFarm.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("roadMyVillage"))
        {
            roadToMyFarm.SetActive(true);
            beginArea.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("beach"))
        {
            beach.SetActive(true);
            town.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("town"))
        {
            town.SetActive(true);
            beach.SetActive(false);
            myFarm.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("outtown"))
        {
            roadToMyFarm.SetActive(true);
            dungeon.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("farm"))
        {
            myFarm.SetActive(true);
            town.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("dungeon"))
        {
            dungeon.SetActive(true);
            AudioManager.instance.PlaySfx("Dungeon");
        }
        else if (collision.gameObject.CompareTag("outDungeon"))
        {
            town.SetActive(true);
        }
    }

    private void SetActiveObjects(bool active, params GameObject[] gameObjects)
    {
        foreach (var obj in gameObjects)
        {
            obj.SetActive(active);
        }
    }
}
