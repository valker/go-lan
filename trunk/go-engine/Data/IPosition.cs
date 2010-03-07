using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public interface IPosition
    {
        MokuField Field { get; }
        bool IsEditable { get; }
        IPosition CopyMokuField();

        /// <summary>
        /// Выполняет ход по правилам Го
        /// </summary>
        /// <param name="point">точка, в которую ходят</param>
        /// <param name="player">игрок, который делает ход</param>
        /// <returns>новая позиция и число съеденных камней</returns>
        Pair<IPosition, int> Move(Point point, MokuState player);

        IPosition GetParentPosition();

        System.Collections.Generic.IEnumerable<IPosition> GetChildrenPositions();

        void SetParent(IPosition parent);

        void AddChild(IPosition child);

        System.Collections.Generic.IDictionary<MokuState, int> GetTerritories();
    }
}