using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.WebAPI.Models
{
    [Table("ToDo")]
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime? TargetDate { get; set;}

        public bool IsCompleted { get; set; }

        public DateTime? CompletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
