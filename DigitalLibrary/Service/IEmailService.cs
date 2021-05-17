using DigitalLibrary.Models;
using System.Threading.Tasks;

namespace DigitalLibrary.Service
{
    public interface IEmailService
    {
        Task SendEmail(UserEmailOptions userEmailOptions);
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}