using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Api
{
    /// <summary>
    /// Define the owning of some field object (stone,territory, etc.)
    /// </summary>
    public enum Stone
    {
        /// <summary>
        /// nobody
        /// </summary>
        None = 0,
        /// <summary>
        /// black
        /// </summary>
        Black = 1,
        /// <summary>
        /// white
        /// </summary>
        White = 2,
        /// <summary>
        /// black and white
        /// </summary>
        Both = 3
    }
}
