namespace Valker.PlayOnLan.GoPlugin.Abstract
{
    public interface ICoordinatesFactory
    {
        ICoordinates Create(int[] parts);
    }
}
