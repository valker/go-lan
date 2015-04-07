using Valker.PlayOnLan.Api;

namespace Valker.PlayOnLan.GoPlugin
{
    public class EmptyCell : ICell
    {
        public override bool Equals(object obj)
        {
            EmptyCell other = obj as EmptyCell;
            if (other == null) return false;
            return true;
        }
        public override string ToString()
        {
            return "EMPTY";
        }
    }
}