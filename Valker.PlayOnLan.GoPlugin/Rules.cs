namespace Valker.PlayOnLan.GoPlugin
{
    public class Rules : IRules
    {
        public KoRule Ko { get; set; }
        public ScoreRule Score { get; set; }
    }
}