using System;
using SQLite;

namespace ToDoManager.Models
{
    [Table("Tasks")]
    public class Item
    {
        [PrimaryKey]
        public string Id { get; set; }
        public uint ParentId { get; set; }
        [MaxLength(100)]
        public string Label { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        public byte Importance { get; set; }
        public byte Urgency { get; set; }
        public float Priority { get; set; }
    }
}