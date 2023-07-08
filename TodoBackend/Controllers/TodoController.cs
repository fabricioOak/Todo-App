using System.Runtime.Intrinsics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoBackend.Data;
using TodoBackend.Models;
using TodoBackend.ViewModels;

namespace TodoBackend.Controllers
{
  [ApiController]
  [Route("v1")]
  public class TodoController : ControllerBase
  {
    [HttpGet]
    [Route("todos")]
    public async Task<IActionResult> GetManyAsync(
      [FromServices] AppDbContext context)
    {
      var todos = await context
      .Todos
      .AsNoTracking()
      .ToListAsync();

      return Ok(todos);

    }
    [HttpGet]
    [Route("todos/{id}")]
    public async Task<IActionResult> GetByIdAsync(
      [FromServices] AppDbContext context,
      [FromRoute] int id)
    {
      var todo = await context
      .Todos
      .FirstOrDefaultAsync(x => x.Id == id);

      return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    [Route("todos")]
    public async Task<IActionResult> PostAsync(
      [FromServices] AppDbContext context,
      [FromBody] CreateTodoViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest();

      var todo = new Todo
      {
        Title = model.Title,
        Content = model.Content,
        Done = false
      };

      try
      {
        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();
        return Created("v1/todos", todo);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut]
    [Route("todos/{id}")]
    public async Task<IActionResult> UpdateAsync(
      [FromServices] AppDbContext context,
      [FromRoute] int id)
    {
      if (!ModelState.IsValid) return BadRequest();

      var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

      if (todo == null) return NotFound();

      try
      {
        todo.Done = !todo.Done;

        context.Todos.Update(todo);
        await context.SaveChangesAsync();

        return Ok(todo);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
    [HttpDelete]
    [Route("todos/{id}")]
    public async Task<IActionResult> DeleteAsync(
      [FromServices] AppDbContext context,
      [FromRoute] int id)
    {
      if (!ModelState.IsValid) return BadRequest();

      var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

      if (todo == null) return NotFound();

      try
      {
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();

        return Ok("Todo deleted");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
