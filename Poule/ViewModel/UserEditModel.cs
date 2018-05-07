using System.ComponentModel.DataAnnotations;

namespace Poule.ViewModel
{
    public class UserEditModel
    {
        [Required]
        [Display(Name = "Volgorde")]
        public int Order { get; set; }
        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Emailadres")]
        public string EmailAddress { get; set; }
    }
}
