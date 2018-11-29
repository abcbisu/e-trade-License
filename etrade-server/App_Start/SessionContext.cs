using etrade.models;
using etrade.services;
using System.Web;
using System;
using etrade;
using System.Linq;

namespace etrade_server.App_Start
{
    public static class SessionContext
    {
        public static UserMin CurrentUser { get { return GetCurrentUser(); } }
        public static Language DefaultLanguage { get { return GetDefaultLanguage(); } }
        public static void SetDefaultLanguage(Language language)
        {
            if (language == null) return;
            HttpContext.Current.Items.Add(SiteConstants.SessionKeyDefLang, language);
        }
        public static void SetUser(UserMin user)
        {
            HttpContext.Current.Items.Add(SiteConstants.SessionKey, user);
        }
        #region
        static UserMin GetCurrentUser()
        {
            return HttpContext.Current.Items[SiteConstants.SessionKey] as UserMin;
        }
        #endregion
        
        private static Language GetDefaultLanguage()
        {
           var lang= HttpContext.Current.Items[SiteConstants.SessionKeyDefLang] as Language;
            if (lang == null)
            {
                var _rscSrvc = new ResourceService(null);
                lang = _rscSrvc.getSupportedLanguages().FirstOrDefault(t => t.IsNatural = true);
                SetDefaultLanguage(lang);
            }
            return lang;
        }
    }
}