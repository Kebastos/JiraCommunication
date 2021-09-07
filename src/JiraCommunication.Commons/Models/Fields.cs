using System.Collections.Generic;

namespace JiraCommunication.Commons.Models
{
    public class Fields
    {
        public Progress Progress { get; set; }
        public string Summary { get; set; }
        public Timetracking Timetracking { get; set; }
        public IssueType IssueType { get; set; }
        public Votes Votes { get; set; }
        public Resolution Resolution { get; set; }
        public List<FixVersion> FixVersions { get; set; }
        public string Resolutiondate { get; set; }
        public int Timespent { get; set; }
        public Author Reporter { get; set; }
        public int AggregateTimeOriginalesTimate { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public string Duedate { get; set; }
        public List<IssueLink> IssueLinks { get; set; }
        public Watches Watches { get; set; }
        public Worklogs Worklog { get; set; }
        public List<Subtask> SubTasks { get; set; }
        public Status Status { get; set; }
        public List<string> Labels { get; set; }
        public long Workratio { get; set; }
        public Author Assignee { get; set; }
        public List<object> Attachment { get; set; }
        public int AggregateTimeEstimate { get; set; }
        public Project Project { get; set; }
        public List<object> Versions { get; set; }
        public string Environment { get; set; }
        public int Timeestimate { get; set; }
        public Aggregateprogress AggregateProgress { get; set; }
        public List<Component> Components { get; set; }
        public Comments Comment { get; set; }
        public int TimeOriginalEstimate { get; set; }
        public int AggregateTimespent { get; set; }
    }
}