using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameAccount : MonoBehaviour
{
    public TMP_InputField edtName;
    public TMP_Text txtError;
    public Selectable fisrt;
    public GameObject ErrorPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
        fisrt.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }

    public void Nameaccount()
    {
        StartCoroutine(Name());
        Name();
    }

    IEnumerator Name()
    {
        //…
        var name = edtName.text;
        var id = Register.registerResponseMoel.id;

        NameModel userModel = new NameModel(id,name);
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/addName", "POST");
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
            NameAccountResponseMoel nameAccountResponseMoel = JsonConvert.DeserializeObject<NameAccountResponseMoel>(jsonString);
            
            if (nameAccountResponseMoel.status)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                ErrorPanel.SetActive(true);
                txtError.text = nameAccountResponseMoel.message;
            }
        }
        request.Dispose();


    }
}
