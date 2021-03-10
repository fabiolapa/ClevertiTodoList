using System;
using System.Collections.Generic;
using System.Text;

namespace ClevertiTodoList.Models
{
    public class Todo
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public bool isDone { get; set; }
        public int priority { get; set; }
    }
}
