using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image imgLoad;
    public float max;
    public float min;
    // Start is called before the first frame update
    void Start()
    {
        
        min = 0;
        StartCoroutine(Load());
    }

    // Update is called once per frame
    void Update()
    {
        //if(min != max)
        //{
        //    imgLoad.fillAmount = (float)min/(float)max;
        //}
    }

    IEnumerator Load()
    {
        while(min < max)
        {
            min += 0.1f;
           imgLoad.fillAmount = min / max;
           yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Đã tải");
        gameObject.SetActive(false);
    }
}
