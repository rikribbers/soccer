using System;
using System.ComponentModel.DataAnnotations;
using Poule.Entities;

namespace Poule.ViewModel
{
    public class PredictionEditModel
    {
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [Display(Name = "Thuisteam")]
        public string HomeTeam { get; set; }

        [Display(Name = "Uitteam")]
        public string AwayTeam { get; set; }

        [Display(Name = "Gebruiker")]
        public string Username { get; set; }

        [Display(Name = "Ruststand"), Required, RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string HalftimeScore { get; set; }

        [Display(Name = "EindStand"), Required, RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string FulltimeScore { get; set; }
    }
}
