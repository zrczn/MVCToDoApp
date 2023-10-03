﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class AudioModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
