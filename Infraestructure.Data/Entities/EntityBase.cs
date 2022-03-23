using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Data.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
