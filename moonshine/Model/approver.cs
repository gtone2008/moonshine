using System;

namespace moonshine.Model
{
    /// <summary>
    /// approver:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class approver
    {
        public approver()
        { }

        #region Model

        private string _costid;
        private string _costname;
        private string _approverntid;
        private string _approvername;

        /// <summary>
        ///
        /// </summary>
        public string costID
        {
            set { _costid = value; }
            get { return _costid; }
        }

        /// <summary>
        ///
        /// </summary>
        public string costName
        {
            set { _costname = value; }
            get { return _costname; }
        }

        /// <summary>
        ///
        /// </summary>
        public string approverNTID
        {
            set { _approverntid = value; }
            get { return _approverntid; }
        }

        /// <summary>
        ///
        /// </summary>
        public string approverName
        {
            set { _approvername = value; }
            get { return _approvername; }
        }

        #endregion Model
    }
}