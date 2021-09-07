namespace JiraCommunication.Commons.Models
{
    /// <summary>
    /// Class representing a Jira attachment
    /// </summary>
    public class Attachment
    {
        public string Self { get; set; }
        public string FileName { get; set; }
        public Author Author { get; set; }
        public string Created { get; set; }
        public int Size { get; set; }
        public string MimeType { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
    }
}
