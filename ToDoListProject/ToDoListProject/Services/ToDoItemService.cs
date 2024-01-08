using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListProject.Data;
using ToDoListProject.Models;

namespace ToDoListProject.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly AppDbContext _context;

        public ToDoItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllItemsAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetItemByIdAsync(int id)
        {
            return await _context.ToDoItems.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<ToDoItem> AddItemAsync(ToDoItem newItem)
        {
            _context.ToDoItems.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }

        public async Task<ToDoItem> UpdateItemAsync(ToDoItem updatedItem)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == updatedItem.Id);

            if (item == null)
            {
                return null;
            }

            item.Title = updatedItem.Title;
            item.Description = updatedItem.Description;
            item.DueDate = updatedItem.DueDate;
            item.IsCompleted = updatedItem.IsCompleted;

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                return false;
            }

            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}