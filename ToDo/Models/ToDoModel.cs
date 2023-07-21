using System.ComponentModel.DataAnnotations;

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
        [FileExtensions(Extensions ="jpg,png", ErrorMessage ="Following extensions only support: jpg, png")]
        public PhotoModel? Photo { get; set; }
        [FileExtensions(Extensions ="mp3", ErrorMessage = "Following extensions only support: mp3")]
        public AudioModel? Audio { get; set; }
        public int RoutineOption { get; set; }

        public DateTime ShowAtSingleDay { get; set; }
        public bool IsItDone { get; set; }

    }
}
