using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using ProgettoIngegneriaSoftware.Extensions;

namespace ProgettoIngegneriaSoftware.Services
{
    public class ConfirmationEmailSenderService : IConfirmationEmailSenderService
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<ConfirmationEmailSenderService> _logger;
        private readonly IConfiguration _configuration;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public ConfirmationEmailSenderService(IConfiguration configuration, ILogger<ConfirmationEmailSenderService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        #endregion CTORS

        #region IConfirmationEmailSenderService IMPLEMENTATION

        public async Task SendConfirmationEmail([EmailAddress] string targetEmail, string confirmationToken, Uri confirmationTokenApiEndpoint)
        {
            var smtpClient = new SmtpClient(_configuration.GetSmtpEmailConfirmationServer())
            {
                Port = _configuration.GetSmtpEmailConfirmationPort(),
                Credentials = new NetworkCredential(_configuration.GetSmtpEmailConfirmationUsername(),
                    _configuration.GetSmtpEmailConfirmationPassword()),
                EnableSsl = true,
            };

            var confirmationTokenApiEndpointUri =
                new Uri(confirmationTokenApiEndpoint, confirmationToken);

            var emailHtmlBodyTemplate =
                string.Concat(await File.ReadAllLinesAsync("./HtmlTemplates/ConfirmationEmailTemplate.html"))
                    .Replace("ConfirmUserEndpointPlaceholder", confirmationTokenApiEndpointUri.AbsoluteUri);


            var mailMessage = new MailMessage(from: "ProgettoIngegneriaSoftware@noreply.com", to: targetEmail,
                subject: "Account Confirmation [Progetto Ingegneria Software]", body: emailHtmlBodyTemplate)
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        #endregion IConfirmationEmailSenderService IMPLEMENTATION

    }
}
