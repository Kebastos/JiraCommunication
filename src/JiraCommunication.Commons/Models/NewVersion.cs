namespace JiraCommunication.Commons.Models
{
    public class NewVersion
    {
        public bool Released { get; set; }
        public string Name { get; set; }
        public string Project { get; set; }
        public string UserReleaseDate { get; set; }
        public string UserStartDate { get; set; }
        public string Description { get; set; }
    }
}