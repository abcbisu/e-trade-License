using etrade.dal;
using etrade.models;
using System.Collections.Generic;
using System.Linq;

namespace etrade.services
{
    interface IResourceService
    {
        void validateUrlPermission(string actionName, string controllerName, string packageName, long userId);
    }
    public class ResourceService: IResourceService
    {
        public long? _actorId { get; private set; }
        public ResourceService( long? actorId)
        {
            _actorId = actorId;
        }
        public void validateUrlPermission(string actionName, string controllerName, string controllerPackage,long userId)
        {
            using(var resource=new Resourcedal(userId))
            {
                if (!resource.hasUrlPermissionForUser(actionName, controllerName, controllerPackage)) throw new AccessDeniedException(string.Format("{0}.{1}.{2} Access Denied", controllerPackage, controllerName, actionName));
            }
            //throw new NotImplementedException();
        }
        public List<models.Language> getSupportedLanguages(int? expresiveLanguageId = null)
        {
            using (var resource = new Resourcedal(_actorId))
            {
               return resource.GetSupportedLanguages(expresiveLanguageId).Select(t=>new Language {
                   Code=t.Code,
                   Id=t.Id,
                   IsNatural=t.IsNatural,
                   Name=t.Name
               }).ToList();
            }
        }
        public object GetLocalizedDatas(List<long> keys, int? langId)
        {
            using (var resource = new Resourcedal(_actorId))
            {
                return resource.GetLocalizedDatas(keys ?? new List<long>(), langId);
            }
        }
    }
}
