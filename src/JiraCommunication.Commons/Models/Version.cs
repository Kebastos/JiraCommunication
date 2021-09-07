using System;

namespace JiraCommunication.Commons.Models
{
    public class Version
    {
        public string Self { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        public bool Released { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string UserReleaseDate { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string ProjectId { get; set; }
    }
}