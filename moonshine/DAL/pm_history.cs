using System.Text;

namespace moonshine.DAL
{
    /// <summary>
    /// 数据访问类:pm_history
    /// </summary>
    public partial class pm_history
    {
        public pm_history()
        { }

        #region Method

        public bool Delete(string ID)
        {
            string strSql = "delete from pm_history where ID =" + ID;
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
        /// 增加一条数据
        /// </summary>
        public bool Add(moonshine.Model.pm_history model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.occur_date != null)
            {
                strSql1.Append("occur_date,");
                strSql2.Append("'" + model.occur_date + "',");
            }
            if (model.location != null)
            {
                strSql1.Append("location,");
                strSql2.Append("'" + model.location + "',");
            }
            if (model.machine_type != null)
            {
                strSql1.Append("machine_type,");
                strSql2.Append("'" + model.machine_type + "',");
            }
            if (model.machine_ID != null)
            {
                strSql1.Append("machine_ID,");
                strSql2.Append("'" + model.machine_ID + "',");
            }
            if (model.costCenter != null)
            {
                strSql1.Append("costCenter,");
                strSql2.Append("'" + model.costCenter + "',");
            }
            if (model.type != null)
            {
                strSql1.Append("type,");
                strSql2.Append("'" + model.type + "',");
            }
            if (model.rootCause != null)
            {
                strSql1.Append("rootCause,");
                strSql2.Append("'" + model.rootCause + "',");
            }
            if (model.solution != null)
            {
                strSql1.Append("solution,");
                strSql2.Append("'" + model.solution + "',");
            }
            if (model.cost != null)
            {
                strSql1.Append("cost,");
                strSql2.Append("'" + model.cost + "',");
            }
            if (model.operator1 != null)
            {
                strSql1.Append("operator,");
                strSql2.Append("'" + model.operator1 + "',");
            }
            if (model.solve_date != null)
            {
                strSql1.Append("solve_date,");
                strSql2.Append("'" + model.solve_date + "',");
            }
            if (model.condition != null)
            {
                strSql1.Append("`condition`,");
                strSql2.Append("'" + model.condition + "',");
            }
            strSql.Append("insert into pm_history(");
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
        public bool Update(moonshine.Model.pm_history model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pm_history set ");
            if (model.occur_date != null)
            {
                strSql.Append("occur_date='" + model.occur_date + "',");
            }
            else
            {
                strSql.Append("occur_date= null ,");
            }
            if (model.location != null)
            {
                strSql.Append("location='" + model.location + "',");
            }
            else
            {
                strSql.Append("location= null ,");
            }
            if (model.machine_type != null)
            {
                strSql.Append("machine_type='" + model.machine_type + "',");
            }
            else
            {
                strSql.Append("machine_type= null ,");
            }
            if (model.machine_ID != null)
            {
                strSql.Append("machine_ID='" + model.machine_ID + "',");
            }
            else
            {
                strSql.Append("machine_ID= null ,");
            }
            if (model.costCenter != null)
            {
                strSql.Append("costCenter='" + model.costCenter + "',");
            }
            else
            {
                strSql.Append("costCenter= null ,");
            }
            if (model.type != null)
            {
                strSql.Append("type='" + model.type + "',");
            }
            else
            {
                strSql.Append("type= null ,");
            }
            if (model.rootCause != null)
            {
                strSql.Append("rootCause='" + model.rootCause + "',");
            }
            else
            {
                strSql.Append("rootCause= null ,");
            }
            if (model.solution != null)
            {
                strSql.Append("solution='" + model.solution + "',");
            }
            else
            {
                strSql.Append("solution= null ,");
            }
            if (model.cost != null)
            {
                strSql.Append("cost='" + model.cost + "',");
            }
            else
            {
                strSql.Append("cost= null ,");
            }
            if (model.operator1 != null)
            {
                strSql.Append("operator='" + model.operator1 + "',");
            }
            else
            {
                strSql.Append("operator= null ,");
            }
            if (model.solve_date != null)
            {
                strSql.Append("solve_date='" + model.solve_date + "',");
            }
            else
            {
                strSql.Append("solve_date= null ,");
            }
            if (model.condition != null)
            {
                strSql.Append("`condition`='" + model.condition + "',");
            }
            else
            {
                strSql.Append("`condition`= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");
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

        #endregion Method
    }
}