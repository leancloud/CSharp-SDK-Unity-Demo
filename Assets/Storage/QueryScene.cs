using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Storage;

public class QueryScene : MonoBehaviour {
    public InputField nameInputField;

    public GameObject contentObject;
    public HeroInfo heroInfoPrefab;

    public async void OnQueryClicked() {
        string name = nameInputField.text;
        if (string.IsNullOrEmpty(name)) {
            return;
        }

        try {
            contentObject.transform.DetachChildren();

            LCQuery<Hero> query = new LCQuery<Hero>("Hero");
            query.WhereContains("name", name);
            ReadOnlyCollection<Hero> heros = await query.Find();
            foreach (Hero hero in heros) {
                HeroInfo heroInfo = Instantiate(heroInfoPrefab);
                heroInfo.Hero = hero;
                heroInfo.transform.SetParent(contentObject.transform);
            }
        } catch (LCException e) {
            Debug.LogError(e);
        }
    }
}
