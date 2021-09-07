using System.Collections.Generic;

namespace JiraCommunication.Commons.Models
{
    public class ProjectMeta
    {
        public string Self { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public List<IssueType> IssueTypes { get; set; }
    }
}