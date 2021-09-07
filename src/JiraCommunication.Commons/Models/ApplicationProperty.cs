namespace JiraCommunication.Commons.Models
{
    /// <summary>
    /// Class representing an Jira application property.
    /// </summary>
    public class ApplicationProperty
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
    }
}
