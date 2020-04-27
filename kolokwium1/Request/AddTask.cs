using kolokwium1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium1.Request
{
    public class AddTask{

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public int IdTeam { get; set; }

        [Required]
        public int IdAssignedTo { get; set; }

        [Required]
        public int IdCreator { get; set; }

        public TaskType taskType { get; set; }
    }
}
