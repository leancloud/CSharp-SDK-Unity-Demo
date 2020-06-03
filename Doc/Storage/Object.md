## 对象

通常我们会用一个专门的表来表示**游戏中的角色**，用来存储游戏人物的属性，比如：

- 角色名
- 头像
- 等级
- ...

Demo 中新建了一个 Hero 表并在代码中创建了对应的类型（参考：子类化）

```csharp
public class Hero : LCObject {
    // 表名
    public Hero() : base("Hero") { }

    public string Name {
        // 字段操作
        get {
            return this["name"] as string;
        } set {
            this["name"] = value;
        }
    }
}
```

创建子类化可以使代码更直观，比如在登录后，创建角色的代码：

```csharp
// 新建英雄对象
Hero hero = new Hero {
    Name = name
};
LCUser currentUser = await LCUser.GetCurrent();
// 将新建的英雄对象和当前用户关联
currentUser["hero"] = hero;
await currentUser.Save();
```
