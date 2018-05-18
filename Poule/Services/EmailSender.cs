

using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

//https://dotnetcoretutorials.com/2017/11/02/using-mailkit-send-receive-email-asp-net-core/

namespace Poule.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IConfiguration _config;

        public EmailSender(IEmailConfiguration emailConfiguration, IConfiguration config)
        {
            _emailConfiguration = emailConfiguration;
            _config = config;
        }

        public Task SendEmailAsync(string email, string subject, string content)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(email, email));
            message.From.Add(new MailboxAddress("De Voetbalpoule", "voetbalpoule@rikribbers.nl"));
            message.Cc.Add(new MailboxAddress("De Voetbalpoule", "voetbalpoule@rikribbers.nl"));

            message.Subject = subject;
            //We will say we are sending HTML. But there are options for plaintext etc. 
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = content
            };

            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using (var emailClient = new SmtpClient())
            {
                // LATER not safe
                emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //The last parameter here is to use SSL (Which you should!)
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                emailClient.Send(message);

                emailClient.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
