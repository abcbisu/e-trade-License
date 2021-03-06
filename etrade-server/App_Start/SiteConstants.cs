﻿using etrade;
using etrade.services;
using etrade.models;
using System.Collections.Generic;
using System.Configuration;

namespace etrade_server.App_Start
{
    public static class SiteConstants
    {
        public static string SessionKey { get { return "{B079E43C-BE52-4700-8C98-94654FB56B56}"; } }
        public static string SessionKeyDefLang { get { return "{AECB1463-485F-41F9-AA13-9F46E2D88209}"; } }
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