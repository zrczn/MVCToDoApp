using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class AudioModel
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public ToDoModel ToDoModel { get; set; }

        [ForeignKey("ToDoModel")]
        public uint ToDoModelId { get; set; }
    }
}
