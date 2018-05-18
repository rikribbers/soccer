namespace Poule.ViewModel
{
    public class ScoreModel
    {
        public ScoreModel()
        {
            HalftimeScore = "-";
            FulltimeScore = "-";
            Points = 0;
        }

        public string FulltimeScore { get; set; }
        public int Points { get; set; }
        public string HalftimeScore { get; set; }
    }
}