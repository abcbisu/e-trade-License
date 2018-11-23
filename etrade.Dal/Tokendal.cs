using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using etrade.entities;
using etrade.models;

namespace etrade.dal
{
    public class Tokendal : DalBase
    {
        public Tokendal(long actorId) : base(actorId) { }
        public entities.UserToken GetToken() 
        {
                var cmd = NewCommand("etrade.Get_UserToken"); //get details from userToken table
                cmd.Parameters.AddWithValue("@userId", _actorId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var lst= GetResult(cmd).Convert<entities.UserToken>();
                if (lst.Count > 0)
                    return lst[0];
                return null;
        }
        public entities.UserToken AddToken(string Token)
        {
                var cmd = NewCommand("etrade.Insert_UserToken"); 
                cmd.Parameters.AddWithValue("@userId", _actorId);
                cmd.Parameters.AddWithValue("@token", Token);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var lst = GetResult(cmd).Convert<entities.UserToken>();
            if (lst.Count > 0)
                return lst[0];
            return null;

        }
        public UserMin GetUserByValidatingToken(string token)
        {
            var cmd = NewCommand("etrade.Get_UserByValidatingToken");
            cmd.Parameters.AddWithValue("@userId", _actorId);
            cmd.Parameters.AddWithValue("@token", token);
            var lst = GetResult(cmd).Convert<UserMin>();
            if (lst.Count > 0)
                return lst[0];
            return null;
        }
        public void KillToken()
        {
            var cmd = NewCommand("etrade.Update_LogoutByKillingToke");
            cmd.Parameters.AddWithValue("@userId", _actorId);
            ExecuteNonQuery(cmd);
        }
    }
}
