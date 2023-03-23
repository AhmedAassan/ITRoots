using ITRoots.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Helper
{
    public class EmailSettings
    {

        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.office365.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("divahmedhassan@outlook.com", "wssass01022");
            client.Send("divahmedhassan@outlook.com", email.To, email.Title, email.Body);
        }
    }
}







//public static void SendEmail(Email email, string apiKey)
//{
//    var client = new SmtpClient("smtp.sendgrid.net", 587);
//    client.EnableSsl = true;
//    client.Credentials = new NetworkCredential("apikey", apiKey);

//    try
//    {
//        using (var message = new MailMessage("divahmedhassan@gmail.com", email.To, email.Title, email.Body))
//        {
//            message.IsBodyHtml = true;
//            client.Send(message);
//        }
//    }
//    catch (Exception ex)
//    {
//        // Handle the error
//        Console.WriteLine($"Error sending email: {ex.Message}");
//    }
//}