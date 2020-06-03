using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour {
    public Text idText;
    public Text nameText;
    public Text createdAtText;

    public Hero Hero {
        set {
            idText.text = value.ObjectId;
            nameText.text = value.Name;
            createdAtText.text = value.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
