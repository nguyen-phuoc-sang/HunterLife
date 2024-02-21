using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public TMP_InputField edtEmail, edtPassword, edtRePass;
    public TMP_Text txtError;
    public Selectable fisrt;
    private EventSystem eventSystem;
    
    public GameObject nameUser, register;
    public static RegisterResponseMoel registerResponseMoel;
    public GameObject ErrorPanel;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        fisrt.Select();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
    }

    public void CheckRegister()
    {
        StartCoroutine(RegisterU());
        RegisterU();
    }

    IEnumerator RegisterU()
    {
        //…
        var email = edtEmail.text;
        var pass = edtPassword.text;
        var repass = edtRePass.text;

            UserModel userModel = new UserModel(email, pass, repass);
            string jsonStringRequest = JsonConvert.SerializeObject(userModel);

            var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/register", "POST");
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
                 registerResponseMoel = JsonConvert.DeserializeObject<RegisterResponseMoel>(jsonString);
                if (registerResponseMoel.status)
                {
                    register.SetActive(false);
                    nameUser.SetActive(true);
                }
                else
                {
                    ErrorPanel.SetActive(true);
                    txtError.text = registerResponseMoel.message;

                }
            }
            request.Dispose();
        
       
    }


}
