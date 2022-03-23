using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Data.Entities
{
    [Table("Contacts")]
    public class Contact : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime Birthdate { get; set; }
        public string ProfileImgUri { get; set; }
        public string Email { get; set; }
        public List<Phone> Phones { get; set; }
        public Address Address { get; set; }
    }
}
