using System.ComponentModel.DataAnnotations;

namespace HealthAPI.Models
{
    public class Image
    {
        /*[Key] --- make it a key property and the corresponding column to a PrimaryKey column in the database*/
        [Key]
        public int ImageID { get; set; }
        public string ImageEncript { get; set; }
    }
}
