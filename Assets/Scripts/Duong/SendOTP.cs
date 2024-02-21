using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SendOTP : MonoBehaviour
{
    public TMP_InputField edtEmail;
    public TMP_Text txtError;
    public GameObject forgotPassword, sendOTP,ErrorPanel;
    public static SendOTPResponseMoel sendOTPResponseMoel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSendOTP()
    {
        StartCoroutine(SendOtp());
        SendOtp();
    }

    IEnumerator SendOtp()
    {
        //…
        var email = edtEmail.text;

        UserModel userModel = new UserModel(email);
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/sendmail", "POST");
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
            sendOTPResponseMoel = JsonConvert.DeserializeObject<SendOTPResponseMoel>(jsonString);
            if (sendOTPResponseMoel.status)
            {
                sendOTP.SetActive(false);
                forgotPassword.SetActive(true);
            }
            else
            {
                ErrorPanel.SetActive(true);
                txtError.text = sendOTPResponseMoel.message;
            }
        }
        request.Dispose();


    }
}
