namespace BstConnectorViewer.Models
{
    public class PressureRecord
    {
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        public bool IsValid { get; set; }
    }
}