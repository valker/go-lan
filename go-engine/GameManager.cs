using System;
using System.Collections.Generic;
using go_engine.Data;
using Microsoft.Xna.Framework;

namespace go_engine
{
    public class GameManager
    {
        private readonly PositionStorage _positionStorage;
        private Dictionary<MokuState, int> _eated;
        public IEnumerable<KeyValuePair<MokuState, int>> Eated { get { return _eated; } }
        private IPosition _currentPosition;

        public GameManager()
        {
            _eated = new Dictionary<MokuState, int>();
            _eated.Add(MokuState.Black, 0);
            _eated.Add(MokuState.White, 0);

            _positionStorage = new PositionStorage(9);
            RootPosition = _positionStorage.Initial;
            CurrentPosition = RootPosition;
            CurrentPlayer = MokuState.Black;
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

        MokuState _currentPlayer=MokuState.Empty;
        public MokuState CurrentPlayer
        {
            get { return _currentPlayer; }
            private set {
                if (_currentPlayer == value) return;
                _currentPlayer = value;
                InvokeCurrentPlayerChanged(EventArgs.Empty);
            }
        }

        public event EventHandler CurrentPositionChanged;
        public event EventHandler CurrentPlayerChanged;
        public event EventHandler EatedChanged;

        private void InvokeCurrentPositionChanged(EventArgs e)
        {
            EventHandler handler = CurrentPositionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void InvokeCurrentPlayerChanged(EventArgs e)
        {
            EventHandler handler = CurrentPlayerChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        private void InvokeEatedChanged(EventArgs e)
        {
            EventHandler handler = EatedChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

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
            CurrentPosition = reply.First;
            if (reply.Second != 0)
            {
                _eated[CurrentPlayer] += reply.Second;
                InvokeEatedChanged(EventArgs.Empty);
            }
            CurrentPlayer = Position.Opposite(CurrentPlayer);
        }

        public IDictionary<MokuState, int> GetTerritories()
        {
            return CurrentPosition.GetTerritories();
        }
    }
}