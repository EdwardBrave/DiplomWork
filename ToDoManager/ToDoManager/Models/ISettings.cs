using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoManager.Models
{
    interface ISettings<K, V>
    {
        void SetValue(K key, V value);
        V GetValue(K key);
        void RemoveValue(K key);
    }
}
