using ClevertiTodoList.Models;
using ClevertiTodoList.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClevertiTodoList.Controllers
{
    [Route("api/clovertiTodos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoListService todoListService;
        public TodoController(ITodoListService todoListService)
        {
            this.todoListService = todoListService;
        }


        [HttpGet]
        [Route("todoList")]
        public ActionResult<List<Todo>> AllTodoLists()
        {

            List<Todo> result = todoListService.GetAllTodoLists(); 
            return Ok(result);
        }
    }
}
