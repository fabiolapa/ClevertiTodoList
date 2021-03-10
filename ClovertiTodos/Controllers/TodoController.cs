using ClevertiTodoList.Models;
using ClevertiTodoList.Services;
using ClovertiTodos.Models;
using ClovertiTodos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Was supposed to get all user info from here but couldnt finish it in time so 
        /// currently userId is shared via Header and login is not needed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] User user)
        {
            //Add future validations

            var token = TokenService.GenerateToken(user);
            return new
            {
                user,
                token
            };
        }

        [HttpGet]
        //[Authorize]
        [Route("todosList")]
        public IActionResult GetUserTodoList([FromHeader] int userID, [FromQuery] int? priority, [FromQuery] bool completed)
        {
            IActionResult result = null;
            try
            { 
                var todos = todoListService.GetAllTodoLists(userID, priority, completed);
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
        //[Authorize]
        [Route("todo")]
        public IActionResult CreateTodo([FromHeader] int userID, [FromBody] Todo todo)
        {
            IActionResult result = null;
            try
            {
                todoListService.InsertTodoList(userID, todo);
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
        //[Authorize]
        [Route("todo")]
        public IActionResult UpdateTodo([FromHeader] int userID, [FromBody] Todo todo)
        {
            IActionResult result = null;
            try
            {
                todoListService.UpdateTodoList(userID, todo);
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
        //[Authorize]
        [Route("todo")]
        public IActionResult DeleteTodo([FromHeader] int userID, [FromHeader] int todoID)
        {
            IActionResult result = null;
            try
            {
                todoListService.DeleteTodoList(userID, todoID);
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
