using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XgagWebsite.Enums;

namespace XgagWebsite
{
    public static class DangerTypeExtensions
    {
        public static string GetBootstrapClass(this DangerType dangerType)
        {
            var result = string.Empty;

            switch (dangerType)
            {
                case DangerType.Info:
                    result = "panel-info";
                    break;
                case DangerType.Warning:
                    result = "panel-warning";
                    break;
                case DangerType.Danger:
                    result = "panel-danger";
                    break;
                default:
                    throw new NotSupportedException();
                    break;
            }

            return result;
        }
    }
}