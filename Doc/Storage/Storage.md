## 存储

### 登录/注册

LeanCloud 支持多种登录方式，包括：

- 账号/密码
- 邮箱
- 手机号/验证码
- 第三方授权

Demo 中展示了最常用的**账号/密码**方式的登录和注册

```csharp
// 登录
LCUser currentUser = await LCUser.Login(username, password);
```

```csharp
// 注册
LCUser user = new LCUser {
    Username = username,
    Password = password
};
await user.SignUp();
```

登录后可以将用户 **session token** 保存在本地，下次直接使用 **session token** 登录。

```csharp
// 保存用户 token
PlayerPrefs.SetString("token", currentUser.SessionToken);
```

```csharp
// 读取本地用户 token 并登录
string sessionToken = PlayerPrefs.GetString("token");
if (!string.IsNullOrEmpty(sessionToken)) {
    try {
        LCUser currentUser = await LCUser.BecomeWithSessionToken(sessionToken);
        await OnLogin(currentUser);
    } catch (LCException e) {
        Debug.LogError(e);
    }
}
```

注销用户

```csharp
LCUser.Logout();
PlayerPrefs.DeleteKey("token");
```

### 对象存储

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
