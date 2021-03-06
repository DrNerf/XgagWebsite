﻿using System.Configuration;

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

        private bool? m_EnableEmailNotifications;

        public bool EnableEmailNotifications
        {
            get
            {
                if (!m_EnableEmailNotifications.HasValue)
                {
                    m_EnableEmailNotifications = bool.Parse(ConfigurationManager.AppSettings[nameof(EnableEmailNotifications)]);
                }

                return m_EnableEmailNotifications.Value;
            }
        }

        private int? m_RankingPeopleCount;

        public int RankingPeopleCount
        {
            get
            {
                if (!m_RankingPeopleCount.HasValue)
                {
                    m_RankingPeopleCount = int.Parse(ConfigurationManager.AppSettings[nameof(RankingPeopleCount)]);
                }

                return m_RankingPeopleCount.Value;
            }
        }

        private int? m_RankingPersistPeriod;

        public int RankingPersistPeriod
        {
            get
            {
                if (!m_RankingPersistPeriod.HasValue)
                {
                    m_RankingPersistPeriod = int.Parse(ConfigurationManager.AppSettings[nameof(RankingPersistPeriod)]);
                }

                return m_RankingPersistPeriod.Value;
            }
        }

        private int? m_MaxDailyVotes;

        public int MaxDailyVotes
        {
            get
            {
                if (!m_MaxDailyVotes.HasValue)
                {
                    m_MaxDailyVotes = int.Parse(ConfigurationManager.AppSettings[nameof(MaxDailyVotes)]);
                }

                return m_MaxDailyVotes.Value;
            }
        }

        private int? m_BaseXP;

        public int BaseXP
        {
            get
            {
                if (!m_BaseXP.HasValue)
                {
                    m_BaseXP = int.Parse(ConfigurationManager.AppSettings[nameof(BaseXP)]);
                }

                return m_BaseXP.Value;
            }
        }

        private int? m_XPMultiplier;

        public int XPMultiplier
        {
            get
            {
                if (!m_XPMultiplier.HasValue)
                {
                    m_XPMultiplier = int.Parse(ConfigurationManager.AppSettings[nameof(XPMultiplier)]);
                }

                return m_XPMultiplier.Value;
            }
        }

        private int? m_XPRewardsCount;

        public int XPRewardsCount
        {
            get
            {
                if (!m_XPMultiplier.HasValue)
                {
                    m_XPMultiplier = int.Parse(ConfigurationManager.AppSettings[nameof(XPMultiplier)]);
                }

                return m_XPMultiplier.Value;
            }
        }

        private int? m_TopStatsCount;

        public int TopStatsCount
        {
            get
            {
                if (!m_TopStatsCount.HasValue)
                {
                    m_TopStatsCount = int.Parse(ConfigurationManager.AppSettings[nameof(TopStatsCount)]);
                }

                return m_TopStatsCount.Value;
            }
        }

        private int? m_ChatPageSize;

        public int ChatPageSize
        {
            get
            {
                if (!m_ChatPageSize.HasValue)
                {
                    m_ChatPageSize = int.Parse(ConfigurationManager.AppSettings[nameof(ChatPageSize)]);
                }

                return m_ChatPageSize.Value;
            }
        }

        private string m_CensorshipText;

        public string CensorshipText
        {
            get
            {
                if (string.IsNullOrEmpty(m_CensorshipText))
                {
                    m_CensorshipText = ConfigurationManager.AppSettings[nameof(CensorshipText)];
                }

                return m_CensorshipText;
            }
        }

        private string m_ProductName;

        public string ProductName
        {
            get
            {
                if (string.IsNullOrEmpty(m_ProductName))
                {
                    m_ProductName = ConfigurationManager.AppSettings[nameof(ProductName)];
                }

                return m_ProductName;
            }
        }

        private string m_ProductLogo;

        public string ProductLogo
        {
            get
            {
                if (string.IsNullOrEmpty(m_ProductLogo))
                {
                    m_ProductLogo = ConfigurationManager.AppSettings[nameof(ProductLogo)];
                }

                return m_ProductLogo;
            }
        }

        private string m_EmailValidator;

        public string EmailValidator
        {
            get
            {
                if (string.IsNullOrEmpty(m_EmailValidator))
                {
                    m_EmailValidator = ConfigurationManager.AppSettings[nameof(EmailValidator)];
                }

                return m_EmailValidator;
            }
        }

        private string m_SlackHook;

        public string SlackHook
        {
            get
            {
                if (string.IsNullOrEmpty(m_SlackHook))
                {
                    m_SlackHook = ConfigurationManager.AppSettings[nameof(SlackHook)];
                }

                return m_SlackHook;
            }
        }

        private string m_ProductFavicon;

        public string ProductFavicon
        {
            get
            {
                if (string.IsNullOrEmpty(m_ProductFavicon))
                {
                    m_ProductFavicon = ConfigurationManager.AppSettings[nameof(ProductFavicon)];
                }

                return m_ProductFavicon;
            }
        }
    }
}