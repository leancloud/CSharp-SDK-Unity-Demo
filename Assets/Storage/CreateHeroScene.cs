using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeanCloud;
using LeanCloud.Storage;

public class CreateHeroScene : MonoBehaviour {
    public InputField nameInputField;

    public Text tipsText;

    public async void OnCreateClicked() {
        string name = nameInputField.text;
        if (string.IsNullOrEmpty(name)) {
            return;
        }

        try {
            Hero hero = new Hero {
                Name = name
            };
            LCUser currentUser = await LCUser.GetCurrent();
            currentUser["hero"] = hero;
            await currentUser.Save();
            SceneManager.LoadScene("Menu");
        } catch (LCException e) {
            Debug.LogError(e);
            tipsText.text = e.ToString();
        }
    }
}
