using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace etrade.dal
{
    public class Resourcedal : DalBase
    {

        public Resourcedal(long? actorId) : base(actorId)
        {
        }
        public bool hasUrlPermissionForUser(string actionName, string controllerName, string packageName)
        {
            var cmd = NewCommand("etrade.get_hasUrlPermissionForUser");
            cmd.Parameters.AddWithValue("@userId", _actorId);
            cmd.Parameters.AddWithValue("@actionName", actionName);
            cmd.Parameters.AddWithValue("@controllerName", controllerName);
            cmd.Parameters.AddWithValue("@packageName", packageName);
            return GetScalar<bool>(cmd);
        }
        public List<entities.Language> GetSupportedLanguages(int? expresiveLanguageId = null)
        {
            var cmd = NewCommand("etrade.get_SupportedLanguages");
            cmd.Parameters.AddWithValue("@expLangId", expresiveLanguageId.ReplaceDbNull());
            return GetResult(cmd).Convert<entities.Language>();
        }
        public object GetLocalizedDatas(List<long> keys, int? langId)
        {

            var cmd = NewCommand("etrade.get_localizedResources");
            cmd.Parameters.AddWithValue("@ids", string.Join(",", keys));
            cmd.Parameters.AddWithValue("@langId", langId.ReplaceDbNull());
            var dt = GetResult(cmd);
            var obj = dt.AsEnumerable().Select(t => new
            {
                id = t.Field<long>("id"),
                LocalizeSrcName = t.Field<string>("LocalizeSrcName"),
                ExpLangId = t.Field<int>("ExpLangId"),
                Data = t.Field<string>("Data"),
                LangId = t.Field<int>("LangId"),
                HolderId = t.Field<int>("HolderId"),
                Code = t.Field<string>("Code")
            }).ToList();

            //group them
            return obj
               .GroupBy(t => new { t.id, t.LocalizeSrcName })
               .Select(g =>
               {
                   //var locD = new models.LocalDatas();
                   var locD = new models.LocalDatas();
                   locD.Id = g.Key.id;
                   locD.About = g.Key.LocalizeSrcName;
                   foreach (var m in g.GroupBy(t => t.Code))
                   {
                       var list = new List<Dictionary<int, string>>();
                       foreach (var x in m)
                       {
                           var dict = new Dictionary<int, string>();
                           dict.Add(x.LangId, x.Data);
                           list.Add(dict);
                       }
                       locD.Datas.Add(m.Key, list);
                   }
                   return locD;

               }).ToList().Select(q=> {
                   var dict = new Dictionary<long, object>();
                   dict.Add(q.Id, q.Datas);
                   return dict;
               });

        }

    }
}
