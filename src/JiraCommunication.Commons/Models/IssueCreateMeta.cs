using System.Collections.Generic;

namespace JiraCommunication.Commons.Models
{
    public class IssueCreateMeta
    {
        public string Expand { get; set; }
        public List<ProjectMeta> Projects { get; set; }
    }
}
