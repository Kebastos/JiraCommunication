namespace JiraCommunication.Commons.Models
{
    public class Worklog
    {
        public string Self { get; set; }
        public Author Author { get; set; }
        public Author UpdateAuthor { get; set; }
        public string Comment { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Started { get; set; }
        public string TimeSpent { get; set; }
        public int TimeSpentSeconds { get; set; }
        public string Id { get; set; }
    }
}