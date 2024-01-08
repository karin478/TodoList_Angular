using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoListProject.Services
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetAllItemsAsync();
        Task<ToDoItem> GetItemByIdAsync(int id);
        Task<ToDoItem> AddItemAsync(ToDoItem newItem);
        Task<ToDoItem> UpdateItemAsync(ToDoItem updatedItem);
        Task<bool> DeleteItemAsync(int id);
    }
}