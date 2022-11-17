using System.ComponentModel.DataAnnotations;

namespace ProgettoIngegneriaSoftware.Services
{

    public interface IConfirmationEmailSenderService
    {
        public Task SendConfirmationEmail([EmailAddress] string targetEmail, string confirmationToken,
            Uri confirmationTokenApiEndpoint);
    }
}