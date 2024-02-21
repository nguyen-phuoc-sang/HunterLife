using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ForgotPassword : MonoBehaviour
{
    public TMP_InputField edtPassword, edtRePass, edtotp;
    public TMP_Text txtError;
    public Selectable fisrt;
    private EventSystem eventSystem;

    public GameObject forgotPassword, login,ErrorPanel;
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

    public void CheckForgotPassword()
    {
        StartCoroutine(Forgotpassword());
        Forgotpassword();
    }

    IEnumerator Forgotpassword()
    {
        //…
        var email = SendOTP.sendOTPResponseMoel.email;
        var pass = edtPassword.text;
        var repass = edtRePass.text;
        var otp = edtotp.text;
        
        if(pass == repass)
        {
                UserModel userModel = new UserModel(email, pass, repass, otp);
                string jsonStringRequest = JsonConvert.SerializeObject(userModel);

                var request = new UnityWebRequest("https://hunterlife-253b1afa0da4.herokuapp.com/api/users/forgotPassword", "POST");
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
                    ForgotPasswordResponseMoel forgotPasswordResponseMoel = JsonConvert.DeserializeObject<ForgotPasswordResponseMoel>(jsonString);
                    if (forgotPasswordResponseMoel.status)
                    {
                        forgotPassword.SetActive(false);
                        login.SetActive(true);
                    }
                    else
                    {
                        ErrorPanel.SetActive(true);
                        txtError.text = forgotPasswordResponseMoel.message;
                    }
                }
                request.Dispose();
        }
        else
        {
            ErrorPanel.SetActive(true);
            txtError.text = "Mật khẩu không khớp";
        }

        


    }
}
