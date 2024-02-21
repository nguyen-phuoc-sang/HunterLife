using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetAllItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void testGetAllItem()
    {
        StartCoroutine(GetDataFromNodeJS());
        GetDataFromNodeJS();
    }

    IEnumerator GetDataFromNodeJS()
    {
        /*var id = "654507e7644da551c636056c";
        TestResponseModel userModel = new TestResponseModel(id);

        string jsonStringRequest = JsonConvert.SerializeObject(userModel);*/

        var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/items", "GET");
        //  byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        //  request.uploadHandler = new UploadHandlerRaw(bodyRaw);
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
            List<AllItemModel> testModels = JsonConvert.DeserializeObject<List<AllItemModel>>(jsonString);


            foreach (AllItemModel model in testModels)
            {
                Debug.Log($"_id: {model._id}");
                Debug.Log($"Item Name: {model.itemName}," +
                     $"Description: {model.description}, " +
                    $"Consumable: {model.consumable}," +
                    $" Image: {model.image}");
                Debug.Log($"Quantity: {model.price}");
            }

        }
        request.Dispose();
    }





























}
