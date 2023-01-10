namespace TodoAPI.Models
{
    public class Todo
    {
        public int? Id { get; set; }
        public string Task { get; set; }
        public int Priority { get; set; }
        public Boolean Completed { get; set; } = false;
        public DateTime? ScheduleAt { get; set; }
    }
}
