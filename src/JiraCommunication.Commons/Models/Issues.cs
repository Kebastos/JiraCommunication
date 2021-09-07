using System.Collections.Generic;

namespace JiraCommunication.Commons.Models
{
    public class Issues
    {
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
        public List<Issue> issues { get; set; }
    }
}
