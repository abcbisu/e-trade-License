using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using etrade.models;

namespace etrade.dal
{
    public class Userdal : DalBase
    {
        public Userdal(long userId):base(userId)
        {
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
