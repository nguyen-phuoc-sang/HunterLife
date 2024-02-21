using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartsControll : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerLife playerLife;

    List<HealthHeart> hearts = new List<HealthHeart>();
    private void Start()
    {
        DrawHeart();
    }
    private void Update()
    {
        DrawHeart();
    }

    public void DrawHeart()
    {
        ClearHearts();
        float maxHealthRemainder = playerLife.CharLifeMax % 2;
        int heartsToMake = (int)((playerLife.CharLifeMax / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();


        }
        for (int i = 0; i < hearts.Count; i++)
        {

            int HeartStatusRemainder = (int)Mathf.Clamp(playerLife.CharLife - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)HeartStatusRemainder);

        }

    }



    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);




    }


    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);

        }
        hearts = new List<HealthHeart>();



    }


}
