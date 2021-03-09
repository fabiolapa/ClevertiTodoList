using ClevertiTodoList.Models;
using ClevertiTodoList.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClevertiTodoList.Services
{
    public interface ITodoListService
    {
        List<Todo> GetAllTodoLists();
    }

    public class TodoListService : ITodoListService
    {
        public ITodotRepository repo;
        public TodoListService(ITodotRepository repo)
        {
            this.repo = repo;
        }

        public List<Todo> GetAllTodoLists()
        {
            return repo.GetAllTodoLists();
        }
    }
}
