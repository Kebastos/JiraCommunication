namespace JiraCommunication.Commons.Models
{
    public class Comment
    {
        public string Self { get; set; }
        public string Id { get; set; }
        public Author Author { get; set; }
        public string Body { get; set; }
        public Author UpdateAuthor { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
    }
}