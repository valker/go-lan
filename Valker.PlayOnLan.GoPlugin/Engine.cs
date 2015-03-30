using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Implements Go rules
    /// </summary>
    public class Engine : IEngine
    {
        /// <summary>
        /// Storage of positions with relations between them
        /// </summary>
        private readonly PositionStorage _positionStorage;

        /// <summary>
        /// Information about eated stones
        /// </summary>
        private readonly Dictionary<Stone, int> _eated;

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
            CurrentPositionChanged?.Invoke(this, e);
        }

        public event EventHandler CurrentPlayerChanged;

        private void InvokeCurrentPlayerChanged(EventArgs e)
        {
            CurrentPlayerChanged?.Invoke(this, e);
        }

        public event EventHandler EatedChanged;

        private void InvokeEatedChanged(EventArgs e)
        {
            EatedChanged?.Invoke(this, e);
        }


        public ICollection<KeyValuePair<Stone, int>> Eated => _eated;

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
        public void Move(Point point)
        {
            var reply = _positionStorage.Move(CurrentPosition, point, CurrentPlayer);

            CheckStoneField(CurrentPosition, reply.First);

            CurrentPosition = reply.First;
            if (reply.Second != 0)
            {
                _eated[CurrentPlayer] += reply.Second;
                InvokeEatedChanged(EventArgs.Empty);
            }
            CurrentPlayer = Util.Opposite(CurrentPlayer);
        }

        public event EventHandler<FieldChangedEventArgs> FieldChanged = delegate {};

        private void InvokeFieldChanged(FieldChangedEventArgs e)
        {
            FieldChanged?.Invoke(this, e);
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
