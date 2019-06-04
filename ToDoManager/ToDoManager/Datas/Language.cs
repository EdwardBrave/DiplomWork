using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoManager.Datas
{
    public class Language
    {
        public string Index { get; set; }
        public string Name { get; set; }

        public Language(string index, string name)
        {
            this.Index = index;
            this.Name = name;
        }
    }
}
