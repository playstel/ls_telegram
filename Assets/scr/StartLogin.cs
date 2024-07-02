using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using scr;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartLogin : WebRequests<TypesRequests.AuthOfRequests>
{
    [SerializeField] private string baseUrl;
    
    [SerializeField] private Button _button;

    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPass;

    [SerializeField] private TMP_Text textResult;

    private void Start()
    {
        _button.onClick.AddListener(TryLogin);

        textResult.text = "";
    }

    private async void TryLogin()
    {
        var obj = new TypesRequests.Login()
        {
            // login = inputEmail.text,
            // password = inputPass.text
        };
            
        string jsonData = JsonUtility.ToJson(obj);
            
        Debug.Log("Login JSON string: " + jsonData);
            
        var result = await PostRequest(baseUrl, TypesRequests.AuthOfRequests.Login, JSON: jsonData);

        textResult.text = $"Login {result.result}";
        
        if(result.result != UnityWebRequest.Result.Success) return;
        
        var answer = JsonUtility.FromJson<TypesRequests.LoginAnswer>(result.downloadHandler.text);

        GUIUtility.systemCopyBuffer = answer.token;
        
        textResult.text = $"Login {result.result}, session token: " + answer.token;
    }
}
