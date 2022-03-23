using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Data.Entities
{
    [Table("Addresses")]
    public class Address
    { 
        [Key]
        public int Id { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Number { get; set; }
        /// <summary>
        /// Floor, letter, any detail information
        /// </summary>
        public string DetailInformation { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
    }
}