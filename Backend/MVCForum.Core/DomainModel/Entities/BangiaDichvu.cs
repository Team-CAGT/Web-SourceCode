using System;

namespace MVCForum.Domain.DomainModel.Entities
{
    public class BangiaDichvu
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public string Type { get; set; }
    }
}
