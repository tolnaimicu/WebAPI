﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using S4_E3.Data;
using S4_E3.Models;

namespace TodosWebAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        public ITodosData todosService;

        public TodosController(ITodosData todosData)
        {
            todosService = todosData;
        }


        [HttpGet]
        public async Task<ActionResult<IList<Todo>>>
            GetTodos([FromQuery] int? userId, [FromQuery] bool? isCompleted)
        {
            try
            {
                IList<Todo> todos = await todosService.GetTodosAsync();
                if (userId != null)
                {
                    todos = todos.Where(todo => todo.UserId == userId).ToList();
                }

                if (isCompleted != null)
                {
                    todos = todos.Where(todo => todo.IsCompleted == isCompleted).ToList();
                }

                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteTodo([FromRoute] int id)
        {
            try
            {
                await todosService.RemoveTodoAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Todo added = await todosService.AddTodoAsync(todo);
                return Created($"/{added.TodoId}", added); // return newly added to-do, to get the auto generated id
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Todo>> UpdateTodo([FromBody] Todo todo)
        {
            try
            {
                Todo updatedTodo = await todosService.UpdateAsync(todo);
                return Ok(updatedTodo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}