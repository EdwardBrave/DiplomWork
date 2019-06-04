using System;
using SQLite;

namespace ToDoManager.Datas
{
    [Table("ArchiveTasks")]
    public class ArchiveItem : Item
    {
        [MaxLength(100)]
        public string Date { get; set; }
        public ArchiveItem(Item baseObj)
        {
            Id = baseObj.Id;
            ParentId = baseObj.ParentId;
            Label = baseObj.Label;
            Category = baseObj.Category;
            Importance = baseObj.Importance;
            Urgency = baseObj.Urgency;
            Priority = baseObj.Priority;
        }

        public ArchiveItem() { }
    }
}
