using ClevertiTodoList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClevertiTodoList.Repositories
{
    public interface ITodotRepository
    {
        public List<Todo> GetAllTodoLists();
    }

    public class FilleTodoListRepository : ITodotRepository
    {
        public List<Todo> GetAllTodoLists()
        {
            return new List<Todo> 
            { 
                new Todo()
                { 
                    ID = 1,
                    Text =  "Test",
                    priority = Priority.Low,
                    isDone = false
                }
            };
        }
    }
}
