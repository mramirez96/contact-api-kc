using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Data.Entities
{
    [Table("Phones")]
    public class Phone
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public PhoneType Type { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }

    }
}