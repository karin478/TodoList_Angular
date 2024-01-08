using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ToDoListProject.Models
{
    public class ToDoItem
    {
        [Key] public int Id { get; set; } // Primary key

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } // Title of the ToDo item

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } // Description of the ToDo item

        public DateTime DueDate { get; set; } // Due date for the ToDo item

        public bool IsCompleted { get; set; } // Status of the ToDo item
    }
}
