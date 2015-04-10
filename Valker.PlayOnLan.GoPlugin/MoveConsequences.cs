namespace Valker.PlayOnLan.GoPlugin
{
    public class MoveConsequences : IMoveConsequences
    {
        public int ScoreDelta { get; set; }
        public IPosition Position { get; set; }
    }
}