using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Models
{
    public class Students
    {
        /*[Key] --- make it a key property and the corresponding column to a PrimaryKey column in the database*/
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public int GroupId { get; set; }
    }
}
