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
        [Route("todosList")]
        public IActionResult GetUserTodoList([FromQuery] int? priority, [FromQuery] bool completed)
        {
            IActionResult result = null;
            try
            { 
                var todos = todoListService.GetAllTodoLists(priority, completed);
                result = Ok(todos);
            }
            catch(Exception e)
            {
                //Log
                result = BadRequest(e);
            }
            
            return result;
        }

        [HttpPost]
        [Route("todo")]
        public IActionResult CreateTodo([FromBody] Todo todo)
        {
            IActionResult result = null;
            try
            {
                todoListService.InsertTodoList(todo);
                result = Ok();
            }
            catch (Exception e)
            {
                //Log
                result = BadRequest(e);
            }
            return result;
        }

        [HttpPut]
        [Route("todo")]
        public IActionResult UpdateTodo([FromBody] Todo todo)
        {
            IActionResult result = null;
            try
            {
                todoListService.UpdateTodoList(todo);
                result = Ok();
            }
            catch (Exception e)
            {
                //Log
                result = BadRequest(e);
            }
            return result;
        }

        [HttpDelete]
        [Route("todo")]
        public IActionResult DeleteTodo([FromHeader] int todoID)
        {
            IActionResult result = null;
            try
            {
                todoListService.DeleteTodoList(todoID);
                result = Ok();
            }
            catch(Exception e)
            {
                //Log
                result = BadRequest(e);
            }
            return result;
        }

      
    }
}
