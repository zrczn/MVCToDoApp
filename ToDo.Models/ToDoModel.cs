using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class ToDoModel
    {
        [Key]
        public uint Id { get; set; }
        [Required(ErrorMessage = "Please enter Topic for your task")]
        [MaxLength(100)]
        public string Topic { get; set; }
        [Required(ErrorMessage ="Please enter Content for your task")]
        [MaxLength(500)]
        public string Contents { get; set; }
        [Range(1,3)]
        public int Priority { get; set; }


        public int PhotoId { get; set; }
        [ValidateNever]
        [ForeignKey("PhotoId")]
        public PhotoModel? Photo { get; set; }


        public int AudioId { get; set; }
        [ValidateNever]
        [ForeignKey("AudioId")]
        public AudioModel? Audio { get; set; }


        public int RoutineOption { get; set; }
        public DateTime ShowAtSingleDay { get; set; }
        public bool IsItDone { get; set; }

        [ValidateNever]
        public string OwnerId { get; set; }
        [ValidateNever]
        [ForeignKey("OwnerId")]
        public ApplicationUser OwnedBy { get; set; }


    }
}
