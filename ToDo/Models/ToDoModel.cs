using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDoModel
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Contents { get; set; }
        [Range(1,3)]
        public int Priority { get; set; }
        public PhotoModel Photo { get; set; }
        public AudioModel Audio { get; set; }
        public int RoutineOption { get; set; }
        public DateTime ShowAtSingleDay { get; set; }

    }
}
