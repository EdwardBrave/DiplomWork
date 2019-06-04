using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ToDoManager.Datas
{
    [Table("Categories")]
    public class Category
    {
        [AutoIncrement, PrimaryKey]
        public uint Id { get; set; }
        [MaxLength(100), Unique]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Section { get; set; }
        public uint Color { get; set; }
        [MaxLength(100)]
        public string Icon { get; set; }
    }
}
