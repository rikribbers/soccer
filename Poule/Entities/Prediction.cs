using System.ComponentModel.DataAnnotations;

namespace Poule.Entities
{
    public class Prediction
    {
        public int Id { get; set; }

        [Required]
        public Game Game { get; set; }

        [Required]
        public User User { get; set; }

        public string HalftimeScore { get; set; }
        public string FulltimeScore { get; set; }
    }
}
