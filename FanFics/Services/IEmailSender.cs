using System.Threading.Tasks;

namespace FanFics.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
