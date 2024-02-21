using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class Transition : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadingTransition()
    {
        StartCoroutine(Loading());
    }

    //Transition Area
    IEnumerator Loading()
    {
        //play animation
        transition.SetTrigger("start");

        //wait
        yield return new WaitForSeconds(transitionTime);


    }
}
