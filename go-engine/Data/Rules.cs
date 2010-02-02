using System;

namespace go_engine.Data
{
    /// <summary>
    /// Класс задаёт правила игры Го
    /// </summary>
    public class Rules
    {
        public KoRule Ko { get; set; }
        public Points Points { get; set; }

        public void Check(Pair<IPosition, int> position, Pair<int, IPosition> distance)
        {
            switch (Ko)
            {
            case KoRule.Simle:
                if (distance.First == 2)
                {
                    throw new GoException(ExceptionReason.Ko);
                }
                return;
            case KoRule.SuperPositioned:
                if (distance.First > 0)
                {
                    throw new GoException(ExceptionReason.Ko);
                }
                return;
            case KoRule.SuperSituation:
                throw new NotImplementedException();
            default:
                throw new InvalidOperationException("Ko rule is not specified.");
            }
        }
    }
}