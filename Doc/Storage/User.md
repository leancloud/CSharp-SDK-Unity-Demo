## 注册，登录/登出

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
