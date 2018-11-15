using System;

namespace etrade.services
{
    interface IResourceService
    {
        void validateUrlPermission(string actionName, string controllerName, string packageName, long userId);
    }
    public class ResourceService: IResourceService
    {
        public void validateUrlPermission(string actionName, string controllerName, string controllerPackage, long userId)
        {
                //check tergate action is available on current roles of tergate user id
                //var roleIds = _db.UsersInRoles
                //    .Where(t => t.UserId == userId && t.IsDeleted == false)
                //    .Select(t => t.RoleId).ToList();
                //var actionIds = _db.ActionsInRoles
                //    .Include(e => e.Action.Controller.ControllerPackage)
                //    .Where(t => t.IsDeleted == false
                //        && t.Action.ActionName == actionName
                //        && t.Action.IsDeleted == false && t.Action.IsActive == true
                //        && t.Action.Controller.ControllerName == controllerName
                //        && t.Action.Controller.IsDeleted == false && t.Action.Controller.IsActive == true
                //        && t.Action.Controller.ControllerPackage.PackageName == packageName
                //        && t.Action.Controller.ControllerPackage.IsDeleted == false
                //        && roleIds.Contains(t.RoleId))
                //    .Select(t => t.ActionId).ToList();
                //if (actionIds.Count() < 1)
                //    throw new AccessDeniedException("Access denied");
            throw new NotImplementedException();
        }
    }
}
