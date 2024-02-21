using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestByShift : MonoBehaviour
{
    public GameObject riuPrefab;
    public GameObject bongPrefab;

    private GameObject currentRiu;
    private GameObject currenBong;

    void Update()
    {
        // Kiểm tra xem người chơi có đang giữ phím Shift hay không
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && !Input.GetKey(KeyCode.Z))
        {
            // Nếu chưa có cây rìu, tạo mới
            if (currentRiu == null)
            {
                currentRiu = Instantiate(riuPrefab);
            }

            // Lấy vị trí của con trỏ chuột
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Di chuyển cây rìu đến vị trí con trỏ chuột
            currentRiu.transform.position = mousePosition;
        }
        else
        {
            // Nếu không giữ phím Shift, hủy cây rìu nếu có
            if (currentRiu != null)
            {
                Destroy(currentRiu);
                currentRiu = null;
            }
        }


        // Kiểm tra xem người chơi có đang giữ phím Z hay không
        if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            // Nếu chưa có cây Cây, tạo mới
            if (currenBong == null)
            {
                currenBong = Instantiate(bongPrefab);
            }

            // Lấy vị trí của con trỏ chuột
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Di chuyển cây bông đến vị trí con trỏ chuột
            currenBong.transform.position = mousePosition;
        }
        else
        {
            // Nếu không giữ phím Z, hủy cây bông nếu có
            if (currenBong != null)
            {
                Destroy(currenBong);
                currenBong = null;
            }
        }
    }
}
