namespace Valker.PlayOnLan.GoPlugin.Abstract
{
    public interface IMove
    {
        IMoveConsequences Perform(IPosition position);
    }
}