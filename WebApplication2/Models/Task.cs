using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Task
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter task name")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Enter task status")]
        public string Status { get; set; }
    }
}
