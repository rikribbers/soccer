using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Poule.Services;

namespace Poule.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Bevestig je emailadres voor De Voetbalpoule",
                $"Bevestig je emailadres door  <a href='{HtmlEncoder.Default.Encode(link)}'>hier</a> te klikken.");
        }

        public static Task SendResetPasswordAsync(this IEmailSender emailSender, string email, string callbackUrl)
        {
            return emailSender.SendEmailAsync(email, "Reset Password",
                $"Reset je wachtwoord door <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>hier</a> te klikken.");
        }
    }
}
