using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using etrade.entities;
using etrade.models;

namespace etrade.dal
{
    public class Userdal : DalBase
    {
        public Userdal(long? userId):base(userId)
        {
        }
        public RspResult<UserMin> GetLoginBehaviourValidatingUser(string identity,string password,IdentityType identityType)
        {
            var cmd = NewCommand("etrade.get_loginBehaviourValidatingUser");
            cmd.Parameters.AddWithValue("@idntity", identity.ReplaceDbNull());
            cmd.Parameters.AddWithValue("@password", password.ReplaceDbNull());
            cmd.Parameters.AddWithValue("@idType", identityType);
            var lst = GetResult(cmd).AsEnumerable().Select(t =>
            {
                var rst = new RspResult<UserMin>
                {
                    CommanDescription=t.Field<string>("cmdDescr"),
                    Exec=t.Field<bool>("exec"),
                    HasPendingCommand=t.Field<bool>("HasPendingCommand"),
                    PendingCommnad=t.Field<string>("nextLoginCommand"),
                    Result=new UserMin() {
                        LoginType=t.Field<LoginType>("LoginType"),
                        UserId=t.Field<long>("UserId")
                    }
                };
                return rst;
            }).ToList();
            if (lst.Count > 0)
                return lst[0];
            return null;
        }
        public UserMin GetUserValidatingCredential(string Idntity, string Password,IdentityType IdType)
        {
            var cmd = NewCommand("etrade.get_UserValidatingCredential");
            cmd.Parameters.AddWithValue("@idntity", Idntity.ReplaceDbNull());
            cmd.Parameters.AddWithValue("@password", Password.ReplaceDbNull());
            cmd.Parameters.AddWithValue("@idType", IdType);
            var lst= GetResult(cmd).Convert<UserMin>();
            if (lst.Count > 0)
                return lst[0];
            return null;
        }
        public bool RecoverUserPassword(string Idntity, IdentityType IdType,  string Password)
        {
            var cmd = NewCommand("etrade.get_RecoverPassword");
            cmd.Parameters.AddWithValue("@idntity", Idntity.ReplaceDbNull());
            cmd.Parameters.AddWithValue("@idType", IdType.ReplaceDbNull());
            cmd.Parameters.AddWithValue("@password", Password.ReplaceDbNull());
            var lst = GetResult(cmd).Convert<bool>();
            if (lst.Count > 0)
                return lst[0];
            return false;
        }
        public UserMin GetUserByIdentity(string Identifire, IdentityType? IdType)
        {
            var cmd = NewCommand("etrade.Get_UserIdentity");
            cmd.Parameters.AddWithValue("@idntity", Identifire.ReplaceDbNull());
            cmd.Parameters.AddWithValue("@identityType", IdType.ReplaceDbNull());
            var lst = GetResult(cmd).Convert<UserMin>();
            if (lst.Count > 0)
                return lst[0];
            return null;
        }
        public void ValidateUserAccLockedStatus(long userId)
        {
            var cmd = NewCommand("etrade.ValidateUserAccLockedStatus");
            cmd.Parameters.AddWithValue("@userId", userId);
            ExecuteNonQuery(cmd);
        }
        
    }
}
