using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BankConsole;

public static class EmailService
{//ikqodpijojvymztb
    public static void SendMail()
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Axel Flores", "axelapp2116@gmail.com"));
        message.To.Add(new MailboxAddress("Axel Ortiz", "u7702364@gmail.com"));
        message.Subject = "BankConsole: Usuarios Nuevos";

        message.Body = new TextPart("plain"){
            Text = GetEmailText()
        };

        using (var client = new SmtpClient ()){
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect ("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("axelapp2116@gmail.com", "jmozjductitlkzka");
            client.Send(message);
            client.Disconnect(true);
        }


    }

    private static string GetEmailText(){
        List<User> newUsers = Storage.GetNewUsers();

        if(newUsers.Count == 0)
            return "No hay usuarios nuevos";

        string emailText = "Usuarios agregados hoy:\n";

        foreach(User user in newUsers)
            emailText += "\t+ " + user.ShowDate() + "\n";
        
        return emailText;
    }
}