using System.Configuration;

namespace XgagWebsite.Helpers
{
    public class ConfigurationHelper
    {
        private static ConfigurationHelper m_Instance;

        public static ConfigurationHelper Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ConfigurationHelper();
                }

                return m_Instance;
            }
        }

        private ConfigurationHelper()
        {

        }

        private int m_PageSize = 0;

        public int PageSize
        {
            get
            {
                if (m_PageSize == 0)
                {
                    m_PageSize = int.Parse(ConfigurationManager.AppSettings[nameof(PageSize)]);
                }

                return m_PageSize;
            }
        }

        private string m_SmtpHost;

        public string SmtpHost
        {
            get
            {
                if (string.IsNullOrEmpty(m_SmtpHost))
                {
                    m_SmtpHost = ConfigurationManager.AppSettings[nameof(SmtpHost)];
                }

                return m_SmtpHost;
            }
        }

        private string m_CredentialsUserName;

        public string CredentialsUserName
        {
            get
            {
                if (string.IsNullOrEmpty(m_CredentialsUserName))
                {
                    m_CredentialsUserName = ConfigurationManager.AppSettings[nameof(CredentialsUserName)];
                }

                return m_CredentialsUserName;
            }
        }

        private string m_CredentialsPassword;

        public string CredentialsPassword
        {
            get
            {
                if (string.IsNullOrEmpty(m_CredentialsPassword))
                {
                    m_CredentialsPassword = ConfigurationManager.AppSettings[nameof(CredentialsPassword)];
                }

                return m_CredentialsPassword;
            }
        }

        private int m_SmtpPort;

        public int SmtpPort
        {
            get
            {
                if (m_SmtpPort == 0)
                {
                    m_SmtpPort = int.Parse(ConfigurationManager.AppSettings[nameof(SmtpPort)]);
                }

                return m_SmtpPort;
            }
        }

        private bool? m_EnableSSL;

        public bool EnableSSL
        {
            get
            {
                if (!m_EnableSSL.HasValue)
                {
                    m_EnableSSL = bool.Parse(ConfigurationManager.AppSettings[nameof(EnableSSL)]);
                }

                return m_EnableSSL.Value;
            }
        }

    }
}