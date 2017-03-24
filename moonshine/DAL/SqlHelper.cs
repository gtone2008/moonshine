using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace moonshine.DAL
{
    public class SqlHelper
    {

        public static readonly string CONN_STRING_DEFAULT =
                    ConfigurationManager.ConnectionStrings["connHRDB"].ConnectionString;

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        #region PrepareCommand等
        /// <summary>
        /// 构造SqlCommand的connection、trans、CommandType、CommandText、Parameters
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="conn">SqlConnection</param>
        /// <param name="trans">SqlTransaction</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">CommandText</param>
        /// <param name="cmdParms">Parameters</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (trans != null)
                conn = trans.Connection;

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 120;

            if (trans != null)
                cmd.Transaction = trans;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 缓冲SqlParameters到全局数组
        /// </summary>
        /// <param name="cacheKey">键值</param>
        /// <param name="cmdParms">参数数组</param>
        public static void CacheParameters(string cacheKey, SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        /// <summary>
        /// 根据键，从缓冲中提取SqlParameters(新克隆的对象)
        /// </summary>
        /// <param name="cacheKey">键值</param>
        /// <returns>SqlParameter[]</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }
        #endregion PrepareCommand

        #region OpenConn
        /// <summary>
        /// 返回缺省的数据库连接
        /// </summary>
        /// <returns>打开的默认数据库连接</returns>
        public static System.Data.SqlClient.SqlConnection OpenConn()
        {
            return OpenConn(CONN_STRING_DEFAULT);
        }

        /// <summary>
        /// 返回指定的数据库连接
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <returns>制定连接字符串代表的数据库连接</returns>
        public static System.Data.SqlClient.SqlConnection OpenConn(string connString)
        {
            try
            {
                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(connString);
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                return sqlConn;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        #endregion OpenConn

        #region OpenTrans
        /// <summary>
        /// 打开一个数据库事务,使用默认的数据库连接(包括打开数据库连接)
        /// </summary>
        /// <returns></returns>
        public static System.Data.SqlClient.SqlTransaction OpenTrans()
        {
            return OpenTrans(CONN_STRING_DEFAULT);
        }

        /// <summary>
        /// 打开一个数据库事务(包括打开数据库连接)
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <returns>打开的数据库事务</returns>
        public static System.Data.SqlClient.SqlTransaction OpenTrans(string connString)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = OpenConn(connString);
                System.Data.SqlClient.SqlTransaction trans = conn.BeginTransaction();
                return trans;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)
        /// 使用默认的数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            int ret = 0;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteNonQuery(conn, null, CommandType.Text, cmdText, null);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)/存储过程
        /// 使用默认的数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            int ret = 0;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteNonQuery(conn, null, cmdType, cmdText, null);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)
        /// 使用默认的数据库连接
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms)
        {
            int ret = 0;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteNonQuery(conn, null, CommandType.Text, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)/存储过程
        /// 使用默认的数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            int ret = 0;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteNonQuery(conn, null, cmdType, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)
        /// 使用指定的数据库连接串创建连接
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(string connString, string cmdText, params SqlParameter[] cmdParms)
        {
            int ret = 0;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn(connString);
                ret = ExecuteNonQuery(conn, null, CommandType.Text, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条返回一个DataSet的SqlCommand命令，通过专用的连接字符串。 
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个DataSet</returns>
        public static DataSet ExcuteDataSet(string connString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            DataSet dataSet = new DataSet();
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dataSet);
                cmd.Parameters.Clear();
                connection.Close();
                GC.Collect();
                return dataSet;
            }
        }

        public static DataSet ExcuteDataSet(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            DataSet dataSet = new DataSet();
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    sda.Fill(dataSet);
                    cmd.Parameters.Clear();
                    //connection.Close();
                    GC.Collect();
                }
            }
            return dataSet;
        }


        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)/存储过程
        /// 使用指定的数据库连接串创建连接
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            int ret = 0;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn(connString);
                ret = ExecuteNonQuery(conn, null, cmdType, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(conn, null, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)/存储过程
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(conn, null, cmdType, cmdText, cmdParms);
        }


        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(trans.Connection, trans, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteNonQuery(trans.Connection, trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句(包括insert,update,delete)/存储过程
        /// 如果trans为空，使用传入的conn，否则使用trans的connection
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回执行sql语句受影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
            int ret = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return ret;
        }
        #endregion ExecuteNonQuery

        #region ExecuteScalar
        /// <summary>
        /// 执行一条sql语句，得到返回的唯一值
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回得到第一行第一列的值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(string cmdText)
        {
            object ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteScalar(conn, null, CommandType.Text, cmdText, null);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的唯一值
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <returns>返回得到第一行第一列的值/存储过程返回值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            object ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteScalar(conn, null, cmdType, cmdText, null);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句，得到返回的唯一值
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms)
        {
            object ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteScalar(conn, null, CommandType.Text, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的唯一值
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值/存储过程返回值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            object ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = ExecuteScalar(conn, null, cmdType, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句，得到返回的唯一值
        /// 使用指定的数据库连接字符串
        /// </summary>
        /// <param name="trans">指定的数据库连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(string connString, string cmdText, params SqlParameter[] cmdParms)
        {
            object ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn(connString);
                ret = ExecuteScalar(conn, null, CommandType.Text, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的唯一值
        /// 使用指定的数据库连接字符串
        /// </summary>
        /// <param name="trans">指定的数据库连接字符串</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值/存储过程返回值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            object ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn(connString);
                ret = ExecuteScalar(conn, null, cmdType, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句，得到返回的唯一值
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(SqlConnection conn, string cmdText, params  SqlParameter[] cmdParms)
        {
            object ret = null;
            ret = ExecuteScalar(conn, null, CommandType.Text, cmdText, cmdParms);
            return ret;
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的唯一值
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值/存储过程返回值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, params  SqlParameter[] cmdParms)
        {
            object ret = null;
            ret = ExecuteScalar(conn, null, cmdType, cmdText, cmdParms);
            return ret;
        }

        /// <summary>
        /// 执行一条sql语句，得到返回的唯一值
        /// 使用指定的数据库事务
        /// </summary>
        /// <param name="conn">指定的数据库事务</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(SqlTransaction trans, string cmdText, params  SqlParameter[] cmdParms)
        {
            object ret = null;
            ret = ExecuteScalar(trans.Connection, trans, CommandType.Text, cmdText, cmdParms);
            return ret;
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的唯一值
        /// 如果trans为空，使用传入的conn，否则使用trans的connection
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>返回得到第一行第一列的值/存储过程返回值，object对象,如果没有记录返回null</returns>
        public static object ExecuteScalar(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params  SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                object ret = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return ret;
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }
        #endregion 	ExecuteScalar

        #region ExecuteReader

        /// <summary>
        /// 执行一条sql语句，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用默认数据库连接字符串
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string cmdText)
        {
            SqlConnection conn = OpenConn(CONN_STRING_DEFAULT);//该资源会在SqlDataReader对象关闭时关闭
            return ExecuteReader(conn, null, CommandType.Text, cmdText, null);
        }
        /// <summary>
        /// 执行一条sql语句/存储过程，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用默认数据库连接字符串
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {
            SqlConnection conn = OpenConn(CONN_STRING_DEFAULT);//该资源会在SqlDataReader对象关闭时关闭
            return ExecuteReader(conn, null, cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行一条sql语句，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用默认数据库连接字符串
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = OpenConn(CONN_STRING_DEFAULT);//该资源会在SqlDataReader对象关闭时关闭
            return ExecuteReader(conn, null, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用默认数据库连接字符串
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = OpenConn(CONN_STRING_DEFAULT);//该资源会在SqlDataReader对象关闭时关闭
            return ExecuteReader(conn, null, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用指定的数据库连接字符串
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string connString, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = OpenConn(connString);//该资源会在SqlDataReader对象关闭时关闭
            return ExecuteReader(conn, null, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用指定的数据库连接字符串
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = OpenConn(connString);//该资源会在SqlDataReader对象关闭时关闭
            return ExecuteReader(conn, null, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteReader(conn, null, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteReader(conn, null, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// </summary>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction trans, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteReader(trans.Connection, trans, CommandType.Text, cmdText, cmdParms);
        }
        /// <summary>
        /// 执行一条sql语句/存储过程，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// </summary>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteReader(trans.Connection, trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到SqlDataReader
        /// 使用后注意关闭 SqlDataReader.Close()
        /// 如果trans为空，使用传入的conn，否则使用trans的connection
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                SqlDataReader SqlReader;
                if (trans == null)
                    //如果事务为空，则关闭SqlDataReader时，自动关闭相关的数据库连接
                    SqlReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                else
                    SqlReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return SqlReader;
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }
        #endregion ExecuteReader

        #region GetDataTableOfRecord
        /// <summary>
        /// 执行一条sql语句，得到返回的DataTable
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>DataTable,异常发生时返回null</returns>
        public static DataTable GetDataTableOfRecord(string cmdText)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                return GetDataTableOfRecord(conn, null, CommandType.Text, cmdText, null);
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的DataTable
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <returns>DataTable,异常：null</returns>
        public static DataTable GetDataTableOfRecord(CommandType cmdType, string cmdText)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                return GetDataTableOfRecord(conn, null, cmdType, cmdText, null);
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条Select语句，得到返回的DataTable
        /// 参数:CommandText,SqlParameters
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdText">CommandText</param>
        /// <param name="cmdParms">SqlParameters</param>
        /// <returns>DataTable,异常发生时返回null</returns>
        public static DataTable GetDataTableOfRecord(string cmdText, params SqlParameter[] cmdParms)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                return GetDataTableOfRecord(conn, null, CommandType.Text, cmdText, cmdParms);
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的DataTable
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>DataTable,异常：null</returns>
        public static DataTable GetDataTableOfRecord(CommandType cmdType, string cmdText, params  SqlParameter[] cmdParms)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                return GetDataTableOfRecord(conn, null, cmdType, cmdText, cmdParms);
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句，得到返回的DataTable
        /// 使用指定的数据库连接字符串
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>DataTable,异常：null</returns>
        public static DataTable GetDataTableOfRecord(string connString, string cmdText, params  SqlParameter[] cmdParms)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConn(connString);
                return GetDataTableOfRecord(conn, null, CommandType.Text, cmdText, cmdParms);
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的DataTable
        /// 使用指定的数据库连接字符串
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>DataTable,异常：null</returns>
        public static DataTable GetDataTableOfRecord(string connString, CommandType cmdType, string cmdText, params  SqlParameter[] cmdParms)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConn(connString);
                return GetDataTableOfRecord(conn, null, cmdType, cmdText, cmdParms);
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条sql语句，得到返回的DataTable
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>DataTable,异常：null</returns>
        public static DataTable GetDataTableOfRecord(SqlConnection conn, string cmdText, params  SqlParameter[] cmdParms)
        {
            return GetDataTableOfRecord(conn, null, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的DataTable
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>DataTable,异常：null</returns>
        public static DataTable GetDataTableOfRecord(SqlConnection conn, CommandType cmdType, string cmdText, params  SqlParameter[] cmdParms)
        {
            return GetDataTableOfRecord(conn, null, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条sql语句/存储过程，得到返回的DataTable
        /// 如果trans为空，使用传入的conn，否则使用trans的connection
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>DataTable,异常：null</returns>
        public static DataTable GetDataTableOfRecord(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params  SqlParameter[] cmdParms)
        {
            SqlDataAdapter ada = new SqlDataAdapter();
            ada.SelectCommand = new SqlCommand();
            DataTable dt = null;
            try
            {
                PrepareCommand(ada.SelectCommand, conn, trans, cmdType, cmdText, cmdParms);
                dt = new DataTable();
                ada.Fill(dt);
                ada.SelectCommand.Parameters.Clear();
            }
            catch (Exception e1)
            {
                dt = null;
                throw e1;
            }
            return dt;
        }
        #endregion GetDataTableOfRecord

        #region GetTopRecord
        /// <summary>
        /// 执行一条Select语句，得到返回的第一条记录数据
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>string [],如果没有记录返回null</returns>
        public static string[] GetTopRecord(string cmdText)
        {
            string[] ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = GetTopRecord(conn, null, CommandType.Text, cmdText, null);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条Select语句，得到返回的第一条记录数据
        /// 使用默认数据库连接
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>string [],如果没有记录返回null</returns>
        public static string[] GetTopRecord(string cmdText, params SqlParameter[] cmdParms)
        {
            string[] ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn();
                ret = GetTopRecord(conn, null, CommandType.Text, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条Select语句，得到返回的第一条记录数据
        /// 使用指定的数据库连接字符串
        /// </summary>
        /// <param name="connString">指定的数据库连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>string [],如果没有记录返回null</returns>
        public static string[] GetTopRecord(string connString, string cmdText, params SqlParameter[] cmdParms)
        {
            string[] ret = null;
            SqlConnection conn = null;
            try
            {
                conn = OpenConn(connString);
                ret = GetTopRecord(conn, null, CommandType.Text, cmdText, cmdParms);
                return ret;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /// <summary>
        /// 执行一条Select语句，得到返回的第一条记录数据
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>string [],如果没有记录返回null</returns>
        public static string[] GetTopRecord(SqlConnection conn, string cmdText, params SqlParameter[] cmdParms)
        {
            return GetTopRecord(conn, null, CommandType.Text, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条Select语句/存储过程，得到返回的第一条记录数据
        /// 使用指定的数据库连接
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>string [],如果没有记录返回null</returns>
        public static string[] GetTopRecord(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return GetTopRecord(conn, null, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行一条Select语句/存储过程，得到返回的第一条记录数据
        /// 如果trans为空，使用传入的conn，否则使用trans的connection
        /// </summary>
        /// <param name="conn">指定的数据库连接</param>
        /// <param name="trans">指定的数据库事务</param>
        /// <param name="cmdType">CommandType</param>
        /// <param name="cmdText">sql语句/存储过程</param>
        /// <param name="cmdParms">SqlCommand 的参数数组</param>
        /// <returns>string [],如果没有记录返回null</returns>
        public static string[] GetTopRecord(SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            string[] ret = null;
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);

            SqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                ret = new string[rs.FieldCount];
                for (int i = 0; i < rs.FieldCount; i++)
                {
                    ret[i] = (rs.GetValue(i) == null) ? string.Empty : rs.GetValue(i).ToString();
                }
            }
            rs.Close();
            return ret;
        }

        #endregion GetTopRecord

    }
}