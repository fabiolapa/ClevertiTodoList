﻿using ClevertiTodoList.Models;
using ClevertiTodoList.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClevertiTodoList.Services
{
    public interface ITodoListService
    {
        public List<Todo> GetAllTodoLists(int? priority, bool searchForCompletedTodos);
        public void InsertTodoList(Todo todo);
        public void DeleteTodoList(int ID);
        public void UpdateTodoList(Todo todo);
    }

    public class TodoListService : ITodoListService
    {
        public ITodotRepository repo;
        public TodoListService(ITodotRepository repo)
        {
            this.repo = repo;
        }

        public List<Todo> GetAllTodoLists(int? priority, bool searchForCompletedTodos)
        {
            var userID = GetInfoFromHeaders();
            var userTodos = repo.GetUserTodoLists(userID);
            
            if(searchForCompletedTodos)
            {
                userTodos = userTodos.Where(todo => todo.isDone).ToList();
            }

            if(priority.HasValue)
            {
                userTodos = userTodos.Where(todo => todo.priority == priority).ToList();
            }

            return userTodos;
        }

        public void InsertTodoList(Todo todo)
        {
            var userID = GetInfoFromHeaders();
            repo.InsertTodoList(userID, todo);
        }

        public void UpdateTodoList(Todo todo)
        {
            var userID = GetInfoFromHeaders();
            repo.UpdateTodoList(userID, todo);
        }

        public void DeleteTodoList(int ID)
        {
            var userID = GetInfoFromHeaders();
            repo.DeleteTodoList(userID, ID);
        }

        //TODO
        private int GetInfoFromHeaders()
        {
            return -1;
        }
    }
}
