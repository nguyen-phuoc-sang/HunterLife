using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildboarManager : MonoBehaviour
{
     public GameObject[] Boar = new GameObject[8];
    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(HoiSinhCoroutine(5));
    }

    // Update is called once per frame
    void Update()
    {
         for (int i = 0; i < Boar.Length; i++)
        {
             
            if (Boar[i].activeSelf)
        {
            //Debug.Log("GameObject đang ở trạng thái active.");
        }
        else
        {
           HoiSinh(i);
        }
    }
        
    }
    IEnumerator HoiSinhCoroutine(int index)
    {
        yield return new WaitForSeconds(10f);

        // Kích hoạt GameObject tại vị trí 'index'
        if (index >= 0 && index < Boar.Length)
        {
            Boar[index].SetActive(true);
        }
    }

    // Hàm này được gọi khi bạn muốn hồi sinh một con lợn cụ thể
    public void HoiSinh(int index)
    {
        // Bắt đầu coroutine hồi sinh
        StartCoroutine(HoiSinhCoroutine(index));
    }
}

