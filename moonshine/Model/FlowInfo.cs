using moonshine.EnumAll;


namespace moonshine.Model
{
    public class FlowInfo
    {
        public int FlowId { get; set; }
        public int ParentId { get; set; }
        public FlowStatus Status { get; set; }
        public int ReqId { get; set; }
    }
}