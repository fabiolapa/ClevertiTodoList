using ClevertiTodoList.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClevertiTodoList.Repositories
{
    public interface ITodotRepository
    {
        public List<Todo> GetUserTodoLists(int userID);
        public void InsertTodoList(int userID, Todo todo);
        public void DeleteTodoList(int userID, int ID);
        public void UpdateTodoList(int userID, Todo todo);
    }

    public class FilleTodoListRepository : ITodotRepository
    {
        private static readonly string DATABASE_FILE = "DB_TODO.txt";

        private Dictionary<int, UserTodosList> usersTodoCache = new Dictionary<int, UserTodosList>();
        //For simplistic reasons and for lack of time our Todo ids will always be the last +1 
        private int _lastID = 0;
        private object _lock = new object();
        private bool isIniatialized = false;

        public List<Todo> GetUserTodoLists(int userID)
        {
            Dictionary<int, Todo> userTodos = new Dictionary<int, Todo>();
            lock (_lock)
            {
                userTodos = GetAllUserTodos(userID);
            }
            return userTodos.Values.Where(todo => !todo.isDone).ToList();
        }

        public void InsertTodoList(int userID, Todo todo)
        {
            if (todo == null)
                throw new Exception("Trying to insert invalid todo");
            todo.UserId = userID;
            todo.ID = ++_lastID;

            lock (_lock)
            {
                var userTodos = GetAllUserTodos(userID);
                userTodos.Add(todo.ID, todo);
                UpdateDatabase();
            }
        }

        public void UpdateTodoList(int userID, Todo todo)
        {
            if (todo == null)
                throw new Exception("Trying to update invalid todo");

            lock (_lock)
            {
                var userTodos = GetAllUserTodos(userID);
                if (!userTodos.TryGetValue(todo.ID, out var cachedTodo))
                    throw new Exception($"Todo {todo.ID} does not exist in DB");

                cachedTodo.isDone = todo.isDone;
                cachedTodo.priority = todo.priority;
                cachedTodo.Text = todo.Text;
                UpdateDatabase();
            }
        }

        public void DeleteTodoList(int userID, int ID)
        {
            lock (_lock)
            {
                var userTodos = GetAllUserTodos(userID);
                if (!usersTodoCache.Remove(ID))
                {
                    //Log deleted with success

                }
                else
                {
                    //Log coldnt delete

                }
                UpdateDatabase();
            }
        }

        private Dictionary<int,Todo> GetAllUserTodos(int userID)
        {
            if (!isIniatialized)
                this.LoadInfoFromDisk();
            if (!usersTodoCache.TryGetValue(userID, out var userTodos))
            {
                userTodos = new UserTodosList() { userID = userID };
                usersTodoCache.Add(userID, userTodos);
            }
            return userTodos.cache;
        }

        /// <summary>
        /// For simplistic reasons every time we write on database
        /// we rewrite the hole cache, unfortunatly i didn't have enough time to design a better solution
        /// </summary>
        private void UpdateDatabase()
        {
            File.WriteAllText(DATABASE_FILE, JsonConvert.SerializeObject(this.usersTodoCache));
        }

        private void LoadInfoFromDisk()
        {
            this.usersTodoCache = JsonConvert.DeserializeObject<Dictionary<int, UserTodosList>>
                                    (File.ReadAllText(DATABASE_FILE));
            if (this.usersTodoCache == null) this.usersTodoCache = new Dictionary<int, UserTodosList>();
        }
    }

    public class UserTodosList
    {
        public int userID { get; set; }
        public Dictionary<int, Todo> cache { get; set; } = new Dictionary<int, Todo>();


    } 
}
