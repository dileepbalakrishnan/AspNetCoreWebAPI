namespace CitiInfo.API.Services
{
    public interface IEmailService
    {
        void Send(string subject, string message);
    }
}