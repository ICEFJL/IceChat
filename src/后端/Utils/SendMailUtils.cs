using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Hosting;

namespace icechat.api.Utils
{
    public static class SendMailUtils
    {
        public static string senderMail = "xxx@qq.com";//发送者邮箱
        public static string senderPassword = "xxxxxx";//授权码
        private static readonly SmtpClient smtpClient = new SmtpClient("smtp.qq.com", 587)//smtp服务器的地址和端口
        {
            Credentials = new NetworkCredential(senderMail, senderPassword),
            EnableSsl = true
        };


        // 发送普通文字邮件
        public static void SendText(string randomNumber, string to)
        {
            try
            {
                var mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(senderMail);
                //接受验证码的邮箱 一般是登录时用户登录的邮箱
                mailMessage.To.Add(to);
                mailMessage.Subject = "用户的验证码";
                mailMessage.IsBodyHtml = true;
                // 使用内联 CSS 样式
                mailMessage.Body = @"<html>
                            <head>
                                <style>
                                    h1 {
                                        color: #333;
                                        font-family: Arial, sans-serif;
                                    }
                                    p {
                                        color: #555;
                                        font-family: Arial, sans-serif;
                                    }
                                    strong {
                                        color: #f00;
                                        font-weight: bold;
                                    }
                                </style>
                            </head>
                            <body>
                                <h1>验证码</h1>
                                <p>您的验证码为：<strong>" + randomNumber + @"</strong></p>
                            </body>
                        </html>";
                smtpClient.Send(mailMessage);
            }
            catch (SmtpException ex)
            {
                // 记录日志或处理异常
                Console.WriteLine($"SMTP Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                // 记录日志或处理异常
                Console.WriteLine($"General Exception: {ex.Message}");
            }
        }
    }
}
