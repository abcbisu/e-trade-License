using etrade.dal;
using System;

namespace etrade.services
{
    interface IResourceService
    {
        void validateUrlPermission(string actionName, string controllerName, string packageName, long userId);
    }
    public class ResourceService: IResourceService
    {
        public long _actorId { get; private set; }
        public ResourceService( long actorId)
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
    }
}
