using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Models
{
    public class ToDos
    {
        /*[Key] --- make it a key property and the corresponding column to a PrimaryKey column in the database*/
        [Key]
        public int ToDoID { get; set; }
        public int EmployeeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
