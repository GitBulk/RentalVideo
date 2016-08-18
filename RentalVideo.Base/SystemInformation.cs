using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalVideo.Base
{
    public class SystemInformation
    {
        public static DateTime GetDate()
        {
            string result = ConfigurationManager.AppSettings["SysDateType"] as string;
            if (!string.IsNullOrEmpty(result) && result.Equals("UtcType"))
            {
                return DateTime.UtcNow;
            }

            return DateTime.Now;
        }
    }
}
