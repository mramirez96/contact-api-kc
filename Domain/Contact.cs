using System.Text.Json.Serialization;

namespace Domain
{
    public class Contact
    {
        public static readonly string DateFormat = "dd/MM/yyyy";

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageAsBase64 { get; set; }
        public string Uri { get; set; }
        public string Birthdate { get; set; }
        public Company Company { get; set; }
        public string Email { get; set; }
        public List<Phone> Phones { get; set; }
        public Address Address { get; set; }
    }
}