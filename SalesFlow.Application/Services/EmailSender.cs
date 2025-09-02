using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Services
{
    public interface ISmtpEmailSender
    {
        Task SendAsync(string toEmail, string subject, string htmlBody);
    }

    public class SmtpEmailSender : ISmtpEmailSender
    {
        private readonly SmtpOptions _opts;
        public SmtpEmailSender(IOptions<SmtpOptions> opts) { _opts = opts.Value; }

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            using var client = new SmtpClient();
            client.Host = _opts.Host;
            client.Port = _opts.Port;
            client.EnableSsl = _opts.EnableSsl;
            client.Credentials = new NetworkCredential(_opts.User, _opts.Password);

            var msg = new MailMessage(_opts.From, toEmail, subject, htmlBody);
            msg.IsBodyHtml = true;

            await client.SendMailAsync(msg);
        }
    }

    public class SmtpOptions
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public string From { get; set; } = "";
    }

}
