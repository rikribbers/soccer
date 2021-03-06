﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Poule.ViewModel
{
    public class PredictionEditModel
    {

        public int Id { get; set; }

        [Display(Name = "Datum")]
        // This is in local timezone
        public DateTime Date { get; set; }

        [Display(Name = "Thuisteam")]
        public string HomeTeam { get; set; }

        [Display(Name = "Uitteam")]
        public string AwayTeam { get; set; }

        [Display(Name = "Gebruiker")]
        public string Username { get; set; }

        [Display(Name = "Ruststand")]
        [Required]
        [RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string HalftimeScore { get; set; }

        [Display(Name = "EindStand")]
        [Required]
        [RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string FulltimeScore { get; set; }

        public bool IsHalftimeScoreValid { get; set; }
        public bool IsFulltimeScoreValid { get; set; }
    }
}
