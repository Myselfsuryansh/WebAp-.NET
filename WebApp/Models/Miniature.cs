using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Miniature
    {
        [Key]
        public int Guid {  get; set; }

        public string? Name { get; set; }

        [DisplayName("Table Ready")]
        public string? TableReady { get; set; }

        public string? Description { get; set; }
    }
}
