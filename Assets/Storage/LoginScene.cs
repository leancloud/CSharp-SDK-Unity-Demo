using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeanCloud;
using LeanCloud.Storage;

public class LoginScene : MonoBehaviour {
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Text tipsText;

    void Awake() {
        Screen.SetResolution(1024, 768, false);
        LCApplication.Initialize("TiuIJfLckzsDeBJrssNNRFVi-gzGzoHsz",
            "uyuRgnt0WQWlWNlhhd83lp0f",
            "https://tiuijflc.lc-cn-n1-shared.com");
        LCLogger.LogDelegate = (level, log) => {
            switch (level) {
                case LCLogLevel.Debug:
                    Debug.Log(log);
                    break;
                case LCLogLevel.Warn:
                    Debug.LogWarning(log);
                    break;
                case LCLogLevel.Error:
                    Debug.LogError(log);
                    break;
                default:
                    break;
            }
        };

        LCObject.RegisterSubclass("Hero", () => new Hero());
    }

    async void Start() {
        string sessionToken = PlayerPrefs.GetString("token");
        if (!string.IsNullOrEmpty(sessionToken)) {
            try {
                LCUser currentUser = await LCUser.BecomeWithSessionToken(sessionToken);
                await OnLogin(currentUser);
            } catch (LCException e) {
                Debug.LogError(e);
            }
        }
    }

    public void OnRegisterClicked() {
        SceneManager.LoadScene("Register");
    }

    public async void OnLoginClicked() {
        string username = usernameInputField.text;
        if (string.IsNullOrEmpty(username)) {
            return;
        }
        string password = passwordInputField.text;
        if (string.IsNullOrEmpty(password)) {
            return;
        }

        try {
            LCUser currentUser = await LCUser.Login(username, password);
            PlayerPrefs.SetString("token", currentUser.SessionToken);
            await OnLogin(currentUser);
        } catch (LCException e) {
            Debug.LogError(e);
            tipsText.text = e.ToString();
        }
    }

    private async Task OnLogin(LCUser user) {
        if (user["hero"] != null) {
            await user.Fetch(includes: new string[] { "hero" });
            SceneManager.LoadScene("Menu");
        } else {
            SceneManager.LoadScene("CreateHero");
        }
    }
}
