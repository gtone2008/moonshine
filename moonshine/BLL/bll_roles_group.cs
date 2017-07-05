using moonshine.Util;

namespace moonshine.BLL
{
    /// <summary>
    /// bll_roles_group
    /// </summary>
    public  class bll_roles_group
    {
        private static readonly moonshine.DAL.dal_roles_group dal = new moonshine.DAL.dal_roles_group();

        public bll_roles_group()
        { }

        #region BasicMethod

        public static string getData()
        {
            string sqlStr = @"select * from roles_group  ";
            return JsonHelper.ToJson(DAL.MysqlHelper.ExecuteReader(sqlStr));
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(moonshine.Model.roles_group model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(moonshine.Model.roles_group model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(string NTID)
        {
            return dal.Delete(NTID);
        }

        #endregion BasicMethod
    }
}