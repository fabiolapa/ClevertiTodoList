using ClevertiTodoList.Models;
using ClevertiTodoList.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClevertiTodoList.Services
{
    public interface ITodoListService
    {
        public List<Todo> GetAllTodoLists(int userID, int? priority, bool searchForCompletedTodos);
        public void InsertTodoList(int userID, Todo todo);
        public void DeleteTodoList(int userID, int todoID);
        public void UpdateTodoList(int userID, Todo todo);
    }

    public class TodoListService : ITodoListService
    {
        public ITodotRepository repo;
        public TodoListService(ITodotRepository repo)
        {
            this.repo = repo;
        }

        public List<Todo> GetAllTodoLists(int userID, int? priority, bool searchForCompletedTodos)
        {
            var userTodos = repo.GetUserTodoLists(userID);
            userTodos = userTodos.Where(todo => todo.isDone == searchForCompletedTodos).ToList();

            if(priority.HasValue)
            {
                userTodos = userTodos.Where(todo => todo.priority == priority).ToList();
            }

            return userTodos;
        }

        public void InsertTodoList(int userID, Todo todo)
        {
            repo.InsertTodoList(userID, todo);
        }

        public void UpdateTodoList(int userID, Todo todo)
        {
            repo.UpdateTodoList(userID, todo);
        }

        public void DeleteTodoList(int userID, int ID)
        {
            repo.DeleteTodoList(userID, ID);
        }
    }
}
