using System;
using LeanCloud.Storage;

public class Hero : LCObject {
    public Hero() : base("Hero") { }

    public string Name {
        get {
            return this["name"] as string;
        } set {
            this["name"] = value;
        }
    }
}
