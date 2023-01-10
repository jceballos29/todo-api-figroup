using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DataContext _context;

        public TodoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetAll()
        {
            return Ok(await _context.Todos.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Create(TodoRequest data)
        {
            var todo = new Todo()
            {
                Task = data.Task,
                Priority= data.Priority,
                ScheduleAt = data.ScheduleAt
            };

            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Todo>> Update([FromRoute] int id, TodoRequest data)
        {
            var todo = await _context.Todos.FindAsync(id);
            if(todo != null) 
            {
                todo.Task= data.Task;
                todo.Priority= data.Priority;
                todo.ScheduleAt = data.ScheduleAt;
                await _context.SaveChangesAsync();
                return Ok(todo);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Todo>> Delete([FromRoute] int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
                return Ok(todo);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("{id:int}/complete")]
        public async Task<ActionResult<Todo>> ToComplete([FromRoute] int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                todo.Completed = true;
                await _context.SaveChangesAsync();
                return Ok(todo);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}/complete")]
        public async Task<ActionResult<Todo>> ToProccess([FromRoute] int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                todo.Completed = false;
                await _context.SaveChangesAsync();
                return Ok(todo);
            }
            return NotFound();
        }

    }
}
