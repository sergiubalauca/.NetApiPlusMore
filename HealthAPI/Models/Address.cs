using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Models
{
    public class Address
    {
        /*[Key] --- make it a key property and the corresponding column to a PrimaryKey column in the database*/
        [Key]
        public int AddressID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
