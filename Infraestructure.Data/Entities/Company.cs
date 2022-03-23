using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Data.Entities
{
    [Table("Companies")]
    public class Company : EntityBase
    {
        [Required]
        public string Name { get; set; }
    }
}