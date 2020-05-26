using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeanCloud;
using LeanCloud.Storage;

public class RegisterScene : MonoBehaviour {
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Text tipsText;

    public void OnBackClicked() {
        SceneManager.LoadScene("Login");
    }

    public async void OnRegisterClicked() {
        string username = usernameInputField.text;
        if (string.IsNullOrEmpty(username)) {
            return;
        }
        string password = passwordInputField.text;
        if (string.IsNullOrEmpty(password)) {
            return;
        }

        LCUser user = new LCUser {
            Username = username,
            Password = password
        };
        try {
            LCUser currentUser = await user.SignUp();
            PlayerPrefs.SetString("token", currentUser.SessionToken);
            // 注册成功
            SceneManager.LoadScene("CreateHero");
        } catch (LCException e) {
            Debug.LogError(e);
            tipsText.text = e.ToString();
        }
    }
}
