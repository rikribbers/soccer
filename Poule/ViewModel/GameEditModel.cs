using System;
using System.ComponentModel.DataAnnotations;
using Poule.Models;

namespace Poule.ViewModel
{
    public class GameEditModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Volgorde")]
        public int Order { get; set; }

        [Required]
        [Display(Name = "Thuisteam")]
        public string HomeTeam { get; set; }

        [Required]
        [Display(Name = "Uitteam")]
        public string AwayTeam { get; set; }

        [Required]
        [Display(Name = "Datum")]
        // This is in local timezone
        public DateTime Date { get; set; }

        [Display(Name = "Ruststand")]
        [RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string HalftimeScore { get; set; }

        [Display(Name = "Eindstand")]
        [RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string FulltimeScore { get; set; }

        [Required]
        [Display(Name = "Ronde")]
        public RoundType Round { get; set; }
    }
}
