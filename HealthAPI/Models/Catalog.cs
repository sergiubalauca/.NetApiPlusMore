using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Models
{
    public class Catalog
    {
        /*[Key]
        [Column(Order = 1)] --- I defined the composite primary key in SQLWorkshopContext, have a look :) */
        public int CourseId { get; set; }
        /*[Key]
        [Column(Order = 2)]*/
        public int StudentId { get; set; }
        public int Grade { get; set; }
    }
}
