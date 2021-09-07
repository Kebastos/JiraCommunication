using System.Collections.Generic;

namespace JiraCommunication.Commons.Models
{
    public class Comments
    {
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
        public List<Comment> comments { get; set; }
    }
}