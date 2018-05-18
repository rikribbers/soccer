using System.ComponentModel.DataAnnotations;

namespace Poule.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
