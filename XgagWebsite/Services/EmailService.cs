using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using XgagWebsite.Helpers;

namespace XgagWebsite.Services
{
    /// <summary>
    /// Service for sending emails.
    /// </summary>
    public class EmailService : IDisposable
    {
        private MailMessage m_MailMessage;
        private SmtpClient m_SmtpClient;

        /// <summary>
        /// Gets or sets the receiving emails.
        /// </summary>
        public IEnumerable<string> ReceivingEmails { get; set; }

        /// <summary>
        /// Gets or sets the SMTP host.
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        public NetworkCredential Credentials { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable SSL.
        /// </summary>
        public bool EnableSSL { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService" /> class.
        /// </summary>
        /// <param name="receivingEmails">The receiving emails.</param>
        /// <param name="smtpHost">The SMTP host.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="port">The port.</param>
        public EmailService(IEnumerable<string> receivingEmails)
        {
            ReceivingEmails = receivingEmails;
            SmtpHost = ConfigurationHelper.Instance.SmtpHost;
            Credentials = new NetworkCredential(
                ConfigurationHelper.Instance.CredentialsUserName,
                ConfigurationHelper.Instance.CredentialsPassword);
            Port = ConfigurationHelper.Instance.SmtpPort;
            EnableSSL = ConfigurationHelper.Instance.EnableSSL;
        }

        /// <summary>
        /// Sends to all receivers. Should be executed in a background thread.
        /// </summary>
        /// <param name="model">The model.</param>
        public void SendToAllReceivers(string message)
        {
            // Command line argument must the the SMTP host.
            m_SmtpClient = new SmtpClient(SmtpHost);
            m_SmtpClient.UseDefaultCredentials = false;
            m_SmtpClient.Port = Port;
            m_SmtpClient.Credentials = Credentials;
            m_SmtpClient.EnableSsl = EnableSSL;
            m_SmtpClient.Timeout = 10000;

            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress(
                "Xgag@naxex-tech.com",
                "Xgag",
                System.Text.Encoding.UTF8);

            // Set destinations for the e-mail message.
            foreach (var receiver in ReceivingEmails)
            {
                MailAddress to = new MailAddress(receiver);

                // Specify the message content.
                m_MailMessage = new MailMessage(from, to);
                m_MailMessage.Body = ConstructEmailBody(message);

                // Include some non-ASCII characters in body and subject.
                m_MailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                m_MailMessage.Subject = "System Message ( ͡° ͜ʖ ͡°)";
                m_MailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

                m_SmtpClient.Send(m_MailMessage);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            m_MailMessage.Dispose();
            m_SmtpClient.Dispose();
        }

        public static string ConstructEmailBody(string message)
        {
            StringBuilder builder = new StringBuilder(message);
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append(string.Format("From: Xgag"));

            return builder.ToString();
        }
    }
}