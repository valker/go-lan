using System;
using System.Diagnostics.Contracts;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    class Pass : IMove
    {
        public IMoveConsequences Perform(IPosition position)
        {
            IPosition newPosition = (IPosition) position.Clone();
            return new PassConsequences(newPosition);
        }
    }

    internal class PassConsequences : IMoveConsequences
    {
        public PassConsequences(IPosition position)
        {
            if(position == null) throw new ArgumentNullException();
            Contract.EndContractBlock();

            Position = position;
        }

        public IPosition Position { get; private set; }
    }
}