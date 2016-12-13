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

    }
}