using System;

namespace JiraCommunication.Commons.Models
{
    public class FixVersion
    {
        public string Self { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Archived { get; set; }
        public string Released { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
