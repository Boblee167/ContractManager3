using System.Threading.Tasks;

namespace ContractManager3.Services
{
    interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
