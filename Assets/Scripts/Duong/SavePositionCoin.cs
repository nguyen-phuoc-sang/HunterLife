using Newtonsoft.Json;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SavePositionCoin : MonoBehaviour
{
    public GameObject savePosition, saveCoin;
    public TMP_Text coin;
    public static int coinn;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        if (Login.loginResponse != null)
        {
            coinn = Login.loginResponse.coin;

            Debug.Log("Login");
        }
        else
        {
            coinn = 0;
        }




    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            savePosition.SetActive(true);
        }*/

        if (Input.GetKeyDown(KeyCode.Y))
        {
            saveCoin.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            coinn += 10;
        }

        if (Input.GetKeyDown(KeyCode.T) && coinn > 0)
        {
            coinn -= 10;
        }


        if (coinn != i)
        {
            SaveCoin();
            i = coinn;
        }

        coin.text = coinn + "";

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SavePosition"))
        {
            savePosition.SetActive(true);
        }
    }

    public void EnablePanel()
    {
        savePosition.SetActive(false);
    }


    public void SavePos()
    {
        StartCoroutine(savePos());
        savePos();
    }

    IEnumerator savePos()
    {
        //…
        var id = "";

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

        var posX = transform.position.x;
        var posY = transform.position.y;
        var posZ = transform.position.z;



        UserModel userModel = new UserModel(id, posX, posY, posZ);

        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/addPosition", "POST");

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
            ResStatus savePosCoinRespModel = JsonConvert.DeserializeObject<ResStatus>(jsonString);

            if (savePosCoinRespModel.status)
            {
                savePosition.SetActive(false);
                Debug.Log("Lưu thành công");
            }
            else
            {
                Debug.Log("Lưu thất bại");
            }


        }
        request.Dispose();


    }

    public void SaveCoin()
    {
        StartCoroutine(saveConi());
        saveConi();
    }

    IEnumerator saveConi()
    {
        //…
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

        var coin = coinn;



        UserModel userModel = new UserModel(id, coin);

        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/addCoin", "POST");

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
            ResStatus savePosCoinRespModel = JsonConvert.DeserializeObject<ResStatus>(jsonString);

            if (savePosCoinRespModel.status)
            {
                saveCoin.SetActive(false);
                Debug.Log("Lưu coin thành công");
            }
            else
            {
                Debug.Log("Lưu thất bại");
            }


        }
        request.Dispose();


    }

}
