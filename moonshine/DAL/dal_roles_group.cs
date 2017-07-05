using System.Text;

namespace moonshine.DAL
{
    /// <summary>
    /// 数据访问类:dal_roles_group
    /// </summary>
    public partial class dal_roles_group
    {
        public dal_roles_group()
        { }

        #region Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(moonshine.Model.roles_group model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.NTID != null)
            {
                strSql1.Append("NTID,");
                strSql2.Append("'" + model.NTID + "',");
            }
            if (model.power != null)
            {
                strSql1.Append("power,");
                strSql2.Append("'" + model.power + "',");
            }
            strSql.Append("insert into roles_group(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            int rows = MysqlHelper.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(moonshine.Model.roles_group model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update roles_group set ");
            if (model.power != null)
            {
                strSql.Append("power='" + model.power + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where NTID='" + model.NTID + "' ");
            int rowsAffected = MysqlHelper.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string NTID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from roles_group ");
            strSql.Append(" where NTID='" + NTID + "' ");
            int rowsAffected = MysqlHelper.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }       /// <summary>

                /// 批量删除数据
                /// </summary>
        public bool DeleteList(string NTIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from roles_group ");
            strSql.Append(" where NTID in (" + NTIDlist + ")  ");
            int rows = MysqlHelper.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Method
    }
}