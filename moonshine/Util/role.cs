using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using moonshine.DAL;
using MySql.Data.MySqlClient;
namespace moonshine.Util
{
    public class role
    {
        enum roles_group { admin, materiel, request, aprive };//管理员、物料员、申请人、审批人              
        public static bool  role_check(string url1)
        {
            bool is_check = false;
            string str1 = "select power from roles_group where NTID='{0}'";
            string str2 = "SELECT b.power,b.url FROM roles_group a right join roles_all b on a.power = b.power where a.NTID = '{0}' and b.url='{1}'";
            str1 = string.Format(str1, Common.GetCurrentNTID());
            str2 = string.Format(str2, Common.GetCurrentNTID(),url1);
            MySqlDataReader mysdr1 = MysqlHelper.ExecuteReader(str1);
            if(mysdr1.Read())
            {
                if (mysdr1.GetValue(0).ToString()=="admin")
                {
                    is_check = true;
                }
                else
                {
                    MySqlDataReader mysdr2 = MysqlHelper.ExecuteReader(str2);
                    if(mysdr2.Read())
                    {
                        is_check = true;
                    }
                    else
                    {
                        is_check = false;
                    }
                }
            }
           
            return is_check;
        }

        public static bool role_checkAdmin()//判断是否是管理员
        {
            bool is_check = false;
            string str1 = "select power from roles_group where NTID='{0}'";
            str1 = string.Format(str1, Common.GetCurrentNTID());
            MySqlDataReader mysdr1 = MysqlHelper.ExecuteReader(str1);
            if (mysdr1.Read())
            {
                if (mysdr1.GetValue(0).ToString() == "admin")
                {
                    is_check = true;
                }
                else
                {                  
                        is_check = false;
                }
            }

            return is_check;
        }
    }
}