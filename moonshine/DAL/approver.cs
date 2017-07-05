using System.Text;

namespace moonshine.DAL
{
    /// <summary>
    /// 数据访问类:approver
    /// </summary>
    public partial class approver
    {
        public approver()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(moonshine.Model.approver model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.costID != null)
            {
                strSql1.Append("costID,");
                strSql2.Append("'" + model.costID + "',");
            }
            if (model.costName != null)
            {
                strSql1.Append("costName,");
                strSql2.Append("'" + model.costName + "',");
            }
            if (model.approverNTID != null)
            {
                strSql1.Append("approverNTID,");
                strSql2.Append("'" + model.approverNTID + "',");
            }
            if (model.approverName != null)
            {
                strSql1.Append("approverName,");
                strSql2.Append("'" + model.approverName + "',");
            }
            strSql.Append("insert into approver(");
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
        public bool Update(moonshine.Model.approver model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update approver set ");
            if (model.costName != null)
            {
                strSql.Append("costName='" + model.costName + "',");
            }
            else
            {
                strSql.Append("costName= null ,");
            }
            if (model.approverNTID != null)
            {
                strSql.Append("approverNTID='" + model.approverNTID + "',");
            }
            if (model.approverName != null)
            {
                strSql.Append("approverName='" + model.approverName + "',");
            }
            else
            {
                strSql.Append("approverName= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where costID='" + model.costID + "' ");
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
        public bool Delete(string costID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from approver ");
            strSql.Append(" where costID='" + costID + "' ");
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
        public bool DeleteList(string costIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from approver ");
            strSql.Append(" where costID in (" + costIDlist + ")  ");
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
    }
}