using Valker.PlayOnLan.Api;

namespace Valker.PlayOnLan.GoPlugin
{
    public class EmptyCell : ICellState
    {
        public Stone GetStone()
        {
            return Stone.None;
        }
    }
}