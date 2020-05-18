using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeanCloud;
using LeanCloud.Storage;

public class LoginScene : MonoBehaviour {
    public InputField clientIdInputField;

    public async void OnLoginClicked() {
        string clientId = clientIdInputField.text;
        if (string.IsNullOrEmpty(clientId)) {
            return;
        }

        Realtime.Init(clientId);
        try {
            await Realtime.Client.Open();
            SceneManager.LoadScene("Main");
        } catch (LCException e) {
            // TODO 处理错误
            Debug.Log($"{e.Code}, {e.Message}");
        }
    }
}
