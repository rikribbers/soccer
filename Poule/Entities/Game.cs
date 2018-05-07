using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace Poule.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string HomeTeam { get; set; }

        [Required]
        public string AwayTeam { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string HalftimeScore { get; set; }
        public string FulltimeScore { get; set; }

        [Required]
        public RoundType Round { get; set; }
    }
}
