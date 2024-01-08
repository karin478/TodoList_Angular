using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListProject.Models;
using ToDoListProject.Services;
using Microsoft.EntityFrameworkCore;

namespace ToDoListProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoItemsController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        // GET: ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllToDoItems()
        {
            var items = await _toDoItemService.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var item = await _toDoItemService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
            var newItem = await _toDoItemService.AddItemAsync(toDoItem);
            return CreatedAtAction(nameof(GetToDoItem), new { id = newItem.Id }, newItem);
        }

        // PUT: ToDoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _toDoItemService.UpdateItemAsync(toDoItem);

            if (updatedItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var deleted = await _toDoItemService.DeleteItemAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}