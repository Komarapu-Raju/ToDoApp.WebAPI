using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.WebAPI.Data;
using ToDoApp.WebAPI.Models;

namespace ToDoApp.WebAPI.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private ToDoDbContext _ToDoDbContext;

        public ToDoController(ToDoDbContext context)
        {
            this._ToDoDbContext = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetToDos()
        {
            var todos = await this._ToDoDbContext.ToDos.Where(todo => !todo.IsDeleted).OrderByDescending(_ => _.UpdatedOn).ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDo(int id)
        {
            var todo = await this._ToDoDbContext.ToDos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpGet("deleted/all")]
        public async Task<IActionResult> GetDeletedToDos()
        {
            var todos = await this._ToDoDbContext.ToDos.Where(todo => todo.IsDeleted).OrderByDescending(_ => _.DeletedOn).ToListAsync();
            return Ok(todos);
        }

        [HttpPost("save")]
        public async Task<IActionResult> AddToDo(ToDo toDo)
        {
            toDo.CreatedOn = DateTime.Now;
            toDo.UpdatedOn = DateTime.Now;
            var todo = await this._ToDoDbContext.ToDos.AddAsync(toDo);
            await this._ToDoDbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateToDo(int  id, ToDo toDo)
        {
            var exisitingToDo = await this._ToDoDbContext.ToDos.FindAsync(id);

            if (exisitingToDo == null)
            {
                return NotFound();
            }

            exisitingToDo.Description = toDo.Description;
            exisitingToDo.TargetDate = toDo.TargetDate;
            exisitingToDo.UpdatedOn = DateTime.Now;
            await this._ToDoDbContext.SaveChangesAsync();
            
            return Ok(toDo);
        }

        [HttpPut("update/status")]
        public async Task<IActionResult> UpdateToDoStatus(int id)
        {
            var existingToDo = await this._ToDoDbContext.ToDos.FindAsync(id);

            if (existingToDo == null)
            {
                return NotFound();
            }

            existingToDo.IsCompleted = !existingToDo.IsCompleted;
            existingToDo.CompletedOn = existingToDo.IsCompleted ? DateTime.Now : null;
            await this._ToDoDbContext.SaveChangesAsync();

            return Ok(existingToDo);
        }

        [HttpPut("undodeletedtodo")]
        public async Task<IActionResult> UndoDeletedToDo(int id)
        {
            var deletedToDo = await this._ToDoDbContext.ToDos.FindAsync(id);

            if (deletedToDo == null)
            {
                return NotFound();
            }

            deletedToDo.IsDeleted = false;
            deletedToDo.DeletedOn = null;
            await this._ToDoDbContext.SaveChangesAsync();

            return Ok(deletedToDo);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            var existingToDo = await this._ToDoDbContext.ToDos.FindAsync(id);

            if (existingToDo == null)
            {
                return NotFound();
            }

            existingToDo.IsDeleted = true;
            existingToDo.DeletedOn = DateTime.Now;
            await this._ToDoDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
