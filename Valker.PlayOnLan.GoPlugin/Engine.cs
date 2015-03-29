using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Implements Go rules
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Storage of positions with relations between them
        /// </summary>
        private PositionStorage _positionStorage;

        /// <summary>
        /// Information about eated stones
        /// </summary>
        private Dictionary<Stone, int> _eated;

        /// <summary>
        /// Komi (added to white result at the scoring stage)
        /// </summary>
        private readonly double _komi;

        public Engine(int size)
        {
            _eated = new Dictionary<Stone, int> {{Stone.Black, 0}, {Stone.White, 0}};

            _komi = 6.5;

            _positionStorage = new PositionStorage(size);
            RootPosition = _positionStorage.Initial;
            CurrentPosition = RootPosition;
            CurrentPlayer = Stone.Black;
        }

        public IPosition CurrentPosition
        {
            get { return _currentPosition; }
            private set
            {
                _currentPosition = value;
                InvokeCurrentPositionChanged(EventArgs.Empty);
            }
        }

        public IPosition RootPosition { get; private set; }

        private Stone _currentPlayer = Stone.None;

        public Stone CurrentPlayer
        {
            get { return _currentPlayer; }
            private set
            {
                if (_currentPlayer == value) return;
                _currentPlayer = value;
                InvokeCurrentPlayerChanged(EventArgs.Empty);
            }
        }


        public event EventHandler CurrentPositionChanged;

        private void InvokeCurrentPositionChanged(EventArgs e)
        {
            EventHandler handler = CurrentPositionChanged;
            if (handler != null) handler(this, e);
        }

        public event EventHandler CurrentPlayerChanged;

        private void InvokeCurrentPlayerChanged(EventArgs e)
        {
            EventHandler handler = CurrentPlayerChanged;
            if (handler != null) handler(this, e);
        }

        public event EventHandler EatedChanged;

        private void InvokeEatedChanged(EventArgs e)
        {
            EventHandler handler = EatedChanged;
            if (handler != null) handler(this, e);
        }


        public IEnumerable<KeyValuePair<Stone, int>> Eated
        {
            get { return _eated; }
        }

        private IPosition _currentPosition;

        public IEnumerable<IPosition> GetChildren(IPosition position)
        {
            return _positionStorage.GetChildPositions(position);
        }

        /// <summary>
        /// Переместиться по дереву позиций
        /// </summary>
        /// <param name="position"></param>
        public void ChangeCurrentPosition(IPosition position)
        {
            CurrentPosition = position;
        }

        /// <summary>
        /// Сделать ход текущим игроком
        /// </summary>
        /// <param name="point"></param>
        public void Move(Utilities.Point point)
        {
            var reply = _positionStorage.Move(CurrentPosition, point, CurrentPlayer);

            CheckStoneField(CurrentPosition, reply.First);

            CurrentPosition = reply.First;
            if (reply.Second != 0)
            {
                _eated[CurrentPlayer] += reply.Second;
                InvokeEatedChanged(EventArgs.Empty);
            }
            CurrentPlayer = Utilities.Util.Opposite(CurrentPlayer);
        }

        public event EventHandler<FieldChangedEventArgs> FieldChanged = delegate {};

        private void InvokeFieldChanged(FieldChangedEventArgs e)
        {
            EventHandler<FieldChangedEventArgs> handler = FieldChanged;
            if (handler != null) handler(this, e);
        }

        private void CheckStoneField(IPosition old, IPosition next)
        {
            var diff = old.CompareStoneField(next);
            foreach (var pair in diff)
            {
               InvokeFieldChanged(new FieldChangedEventArgs(pair)); 
            }
        }

        public void Pass()
        {
            throw new NotImplementedException();
        }
    }
}
