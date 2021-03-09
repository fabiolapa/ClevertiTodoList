using System;
using System.Collections.Generic;
using System.Text;

namespace ClevertiTodoList.Models
{
    public class Todo
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool isDone { get; set; }
        public Priority priority { get; set; }
    }

    public enum Priority
    {
        High = 2,
        Medium = 1,
        Low = 0
    }
}
