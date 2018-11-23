using System;

namespace etrade.dal
{
    public class Resourcedal : DalBase
    {
        
        public Resourcedal(long actorId) :base(actorId)
        {
        }
        public bool hasUrlPermissionForUser(string actionName,string controllerName,string packageName)
        {
            var cmd = NewCommand("etrade.get_hasUrlPermissionForUser");
            cmd.Parameters.AddWithValue("@userId", _actorId);
            cmd.Parameters.AddWithValue("@actionName", actionName);
            cmd.Parameters.AddWithValue("@controllerName", controllerName);
            cmd.Parameters.AddWithValue("@packageName", packageName);
            return GetScalar<bool>(cmd);
        }
    }
}
