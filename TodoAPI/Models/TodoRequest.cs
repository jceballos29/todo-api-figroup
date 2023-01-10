namespace TodoAPI.Models
{
    public class TodoRequest
    {
        public string Task { get; set; }
        public int Priority { get; set; }
        public DateTime? ScheduleAt { get; set; }
        public int ProjectId { get; set; }
    }
}
