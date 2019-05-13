using System.Diagnostics;

namespace CitiInfo.API.Services
{
    public class IntranetEmailService : IEmailService
    {
        private readonly string _from = Startup.Configuration["emailSettings:mailFromAddress"];
        private readonly string _to = Startup.Configuration["emailSettings:mailToAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"An email from {_from} to {_to}");
            Debug.WriteLine(subject);
            Debug.WriteLine(message);
        }
    }
}