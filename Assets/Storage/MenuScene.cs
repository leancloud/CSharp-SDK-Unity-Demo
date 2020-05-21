using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeanCloud;
using LeanCloud.Storage;

public class MenuScene : MonoBehaviour {
    public Text welcomeText;

    async void Start() {
        LCUser currentUser = await LCUser.GetCurrent();
        Hero hero = currentUser["hero"] as Hero;
        welcomeText.text = $"欢迎<b>{hero.Name}</b>来到 LeanCloud";
    }

    public void OnChatClicked() {
        SceneManager.LoadScene("Chat");
    }
}
