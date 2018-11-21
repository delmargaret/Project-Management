using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BLL.Services
{
    public class EmailService
    {
        public void SendMail(string email)
        {
            MailAddress from = new MailAddress("appprojectmanagement@gmail.com");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Менеджер проектов";
            m.Body = "<h2>Добро пожаловать!</h2>"+
                "<p>Вы были приглашены в систему <q>менеджер проектов</q>.</p>"+
                "<p>Для того, чтобы продолжить, пройдите регистрацию по <a href='http://localhost:12323/registration'>ссылке</a>.</p>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("appprojectmanagement@gmail.com", "zsvspsosnyhmtwln");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
