# Icechat

简易聊天系统，包括客户端，网页端和后端

后端由C# Webapi等技术实现服务器端逻辑

客户端，网页端由Vue和electron打包实现

参考：[Juanbai7877/Wschat: 简易聊天系统，包括客户端，网页端和后端](https://github.com/Juanbai7877/Wschat?tab=readme-ov-file)

## **使用邮箱注册功能**

如果使用邮箱注册发送验证码功能，需要在IceChat\src\后端\Utils\SendMailUtils.cs中设置对应参数

```
public static string senderMail = "xxx@qq.com";//发送者邮箱

public static string senderPassword = "xxxxxx";//授权码

private static readonly SmtpClient smtpClient = new SmtpClient("smtp.qq.com", 587)//smtp服务器的地址和端口

{

Credentials = new NetworkCredential(senderMail, senderPassword),

EnableSsl = true

};
```


