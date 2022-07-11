namespace UserService.Models
{
    public class Task
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string ResponsibleUser { get; set; }

        public DateTimeOffset StartDateTime { get; set; }

        public DateTimeOffset EndDateTime { get; set; }
    }
}
