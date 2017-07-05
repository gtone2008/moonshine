using System;

namespace moonshine.Model
{
    /// <summary>
    /// pm_history:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pm_history
    {
        public pm_history()
        { }

        #region Model

        private int _id;
        private string _occur_date;
        private string _location;
        private string _machine_type;
        private string _machine_id;
        private string _costcenter;
        private string _type;
        private string _rootcause;
        private string _solution;
        private string _cost;
        private string _operator;
        private string _solve_date;
        private string _condition;

        /// <summary>
        /// auto_increment
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        ///
        /// </summary>
        public string occur_date
        {
            set { _occur_date = value; }
            get { return _occur_date; }
        }

        /// <summary>
        ///
        /// </summary>
        public string location
        {
            set { _location = value; }
            get { return _location; }
        }

        /// <summary>
        ///
        /// </summary>
        public string machine_type
        {
            set { _machine_type = value; }
            get { return _machine_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string machine_ID
        {
            set { _machine_id = value; }
            get { return _machine_id; }
        }

        /// <summary>
        ///
        /// </summary>
        public string costCenter
        {
            set { _costcenter = value; }
            get { return _costcenter; }
        }

        /// <summary>
        ///
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string rootCause
        {
            set { _rootcause = value; }
            get { return _rootcause; }
        }

        /// <summary>
        ///
        /// </summary>
        public string solution
        {
            set { _solution = value; }
            get { return _solution; }
        }

        /// <summary>
        ///
        /// </summary>
        public string cost
        {
            set { _cost = value; }
            get { return _cost; }
        }

        /// <summary>
        ///
        /// </summary>
        public string operator1
        {
            set { _operator = value; }
            get { return _operator; }
        }

        /// <summary>
        ///
        /// </summary>
        public string solve_date
        {
            set { _solve_date = value; }
            get { return _solve_date; }
        }

        /// <summary>
        ///
        /// </summary>
        public string condition
        {
            set { _condition = value; }
            get { return _condition; }
        }

        #endregion Model
    }
}