using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using LeanCloud;
using LeanCloud.Storage;
using LeanCloud.Realtime;

public static class Realtime {
    public static LCIMClient Client {
        get; set;
    }

    private static readonly Dictionary<string, Hero> id2HeroDict = new Dictionary<string, Hero>();
    private static readonly Dictionary<string, Hero> name2HeroDict = new Dictionary<string, Hero>();

    public static async Task Login(string heroId) {
        Client = new LCIMClient(heroId);
        await Client.Open();
    }

    public static async Task<Hero> GetHeroById(string id) {
        if (!id2HeroDict.TryGetValue(id, out Hero hero)) {
            // 查询
            LCQuery<Hero> query = new LCQuery<Hero>("Hero");
            hero = await query.Get(id);
            id2HeroDict[id] = hero;
            name2HeroDict[hero.Name] = hero;
        }
        return hero;
    }

    public static async Task<Hero> GetHeroByName(string name) {
        if (!name2HeroDict.TryGetValue(name, out Hero hero)) {
            // 查询
            LCQuery<Hero> query = new LCQuery<Hero>("Hero");
            query.WhereEqualTo("name", name);
            hero = await query.First();
            if (hero == null) {
                throw new Exception("查无此人");
            }
            id2HeroDict[hero.ObjectId] = hero;
            name2HeroDict[hero.Name] = hero;
        }
        return hero;
    }
}
