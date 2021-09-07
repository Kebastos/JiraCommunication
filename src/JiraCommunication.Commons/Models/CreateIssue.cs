using System.Collections.Generic;

namespace JiraCommunication.Commons.Models
{
    /// <summary>
    /// Class used to create a new issue in the Jira API.
    /// 
    /// Contains only a dictionary because that allows us to (easily) add any custom fields
    /// without having trouble with serialization and deserialization. That's why Issue is not used
    /// for creating issues.
    /// </summary>
    public class CreateIssue
    {
		//[UsedImplicitly(ImplicitUseKindFlags.Access)]
	    public readonly Dictionary<string, object> fields;

        public CreateIssue(
            string ProjectKey,
            string Summary,
            string Description,
            string IssueTypeId,
            string PriorityId,
            IEnumerable<string> labels)
        {
            fields = new Dictionary<string, object>
            {
                { "project", new { key = ProjectKey } },
                { "summary", Summary },
                { "description", Description },
                { "issuetype", new { id = IssueTypeId } },
                { "priority", new { id = PriorityId } },
                { "labels", labels }
            };
        }

        public void AddField(string fieldName, object value)
        {
            fields.Add(fieldName, value);
        }
    }
}
