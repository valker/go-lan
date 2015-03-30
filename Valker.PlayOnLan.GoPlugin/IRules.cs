using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IRules
    {
        bool IsAcceptable(Tuple<IPosition, IMoveInfo> newPosition, Pair<int, IPosition> distance);
    }
}
