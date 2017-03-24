using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
//using MySql.Data.MySqlClient;
using System.IO;
using System.Net;
using System.Text;

namespace moonshine.DAL
{
    public class dal_aduser
    {
        public Model.AdUser GetUser(string uid)
        {
            string cmdText = "select userid,dept,displayName,mail,site from WinADUser where userId=@uid";
            SqlParameter param = new SqlParameter("@uid", uid);
            return GetUser(cmdText, param);
        }

        private Model.AdUser GetUser(string cmdText, params SqlParameter[] param)
        {
            Model.AdUser user = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(cmdText, param))
            {
                if (reader.Read())
                {
                    user = SqlDataReaderToUser(reader);
                }
            }
            return user;
        }

        public void SaveUser(Model.AdUser user)
        {
            string cmdText = @"insert into WinADUser (userid,displayName,fullName,mail,status,site)
                            values (@UID,@DisplayName,@DisplayName,@Mail,1,@Site)";
            SqlParameter[] paramters = {
                new SqlParameter("@UID",user.UID),
                new SqlParameter("@DisplayName",user.DisplayName),
                new SqlParameter("@Mail",user.Email)
            };
            SqlHelper.ExecuteNonQuery(cmdText, paramters);
        }

        private Model.AdUser SqlDataReaderToUser(SqlDataReader reader)
        {
            Model.AdUser user = new Model.AdUser
            {
                UID = reader["userid"].ToString(),
                Department = reader["dept"].ToString(),
                DisplayName = reader["displayName"].ToString(),
                Email = reader["mail"].ToString()
            };
            return user;
        }
    }
}