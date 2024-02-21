using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Event : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LogoutGame()
    {
        StartCoroutine(logoutApi());
        logoutApi();

    }
    IEnumerator logoutApi()
    {
        var id = "654507e7644da551c636056c";

        if (Login.loginResponse != null)
        {
            id = Login.loginResponse.id;
            Debug.Log("id1" + id);
            Debug.Log("Login");
        }

        if (Register.registerResponseMoel != null)
        {
            id = Register.registerResponseMoel.id;
            Debug.Log("Register" + id);
        }
        TestResponseModel userModel = new TestResponseModel(id);

        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/logout", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            // Đây là cách giải mã mảng JSON thành một danh sách đối tượng TestModel
            ResStatus resStatus = JsonConvert.DeserializeObject<ResStatus>(jsonString);

            if (resStatus.status)
            {
                Debug.Log("true logout");
                // Application.Quit();
            }
            else
            {
                Debug.Log("error logout");
            }

        }
        request.Dispose();
    }
}
