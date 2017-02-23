using System.Threading.Tasks;

namespace WebAppCore.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
