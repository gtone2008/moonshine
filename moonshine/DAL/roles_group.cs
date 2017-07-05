using System;
namespace moonshine.Model
{
    /// <summary>
    /// roles_group:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class roles_group
    {
        public roles_group()
        { }
        #region Model
        private string _ntid;
        private string _power = "0";
        /// <summary>
        /// 
        /// </summary>
        public string NTID
        {
            set { _ntid = value; }
            get { return _ntid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string power
        {
            set { _power = value; }
            get { return _power; }
        }
        #endregion Model

    }
}

