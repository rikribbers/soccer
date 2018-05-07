using System.ComponentModel.DataAnnotations;

namespace Poule.ViewModel
{
    public class MyPredictionEditModel
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        [Display(Name = "Thuisteam")]
        public string HomeTeam { get; set; }

        [Display(Name = "Uitteam")]
        public string AwayTeam { get; set; }

        [Display(Name = "Ruststand"), RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string HalftimeScore { get; set; }

        [Display(Name = "EindStand"), RegularExpression("^[0-9]{1,3}-[0-9]{1,3}$")]
        public string FulltimeScore { get; set; }
    }
}
