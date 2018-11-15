using etrade;
using etrade.services;
using etrade.services.Models;
using System.Collections.Generic;
using System.Configuration;

namespace etrade_server.App_Start
{
    public static class SiteConstants
    {
        public static string SessionKey { get { return "{B079E43C-BE52-4700-8C98-94654FB56B56}"; } }
        public static T UserAnonymous<T>()
        {
            return ConfigurationManager.AppSettings["user-anonymous"].ToString().Deserialize<T>();
        }
        public static T UserSystem<T>()
        {
            return ConfigurationManager.AppSettings["user-system"].ToString().Deserialize<T>();
        }
    }
}