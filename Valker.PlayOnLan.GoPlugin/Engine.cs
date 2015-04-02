using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Annotations;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    ///     Implements Go rules
    /// </summary>
    public class Engine : IEngine
    {
        /// <summary>
        ///     Создать объект "движка"
        /// </summary>
        /// <param name="positionStorage"></param>
        /// <param name="playerProvider"></param>
        /// <param name="rules"></param>
        public Engine(
            IPositionStorage positionStorage,
            IPlayerProvider playerProvider,
            IRules rules)
        {
            PlayerProvider = playerProvider;
            _score = playerProvider.GetPlayers().ToDictionary(player => player.PlayerName, rules.GetInitialScore);
            _rules = rules;
            _positionStorage = positionStorage;
            CurrentPosition = _positionStorage.Initial;
            CurrentPlayer = playerProvider.GetFirstPlayer();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region properties

        public IPosition CurrentPosition
        {
            get { return _currentPosition; }
            private set
            {
                if (Equals(_currentPosition, value)) return;
                _currentPosition = value;
                OnPropertyChanged();
                CurrentPlayer = CurrentPosition.CurrentPlayer;
            }
        }

        public IPlayer CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if (Equals(_currentPlayer, value)) return;
                _currentPlayer = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region events

        public event EventHandler<CellChangedEventArgs> CellChanged;

        public event EventHandler ScoreChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region methods

        public double GetScore(IPlayer player)
        {
            return _score[player.PlayerName];
        }

        public void Move(IMove move)
        {
            Tuple<bool, ExceptionReason> isAcceptable = _rules.IsMoveAcceptableInPosition(move, CurrentPosition);
            if (!isAcceptable.Item1)
            {
                throw new GoException(isAcceptable.Item2);
            }
            Tuple<IPosition, IMoveInfo> newPosition = move.Perform(CurrentPosition);
            isAcceptable = _rules.IsPositionAcceptableInGameLine(newPosition.Item1, _positionStorage);
            if (!isAcceptable.Item1)
            {
                throw new GoException(isAcceptable.Item2);
            }
            _score[CurrentPlayer.PlayerName] += newPosition.Item2.Eated;
            CurrentPosition = newPosition.Item1;
        }

        #endregion

//        /// <summary>
//        ///     Сделать ход текущим игроком
//        /// </summary>
//        /// <param name="point"></param>
//        public void Move(Point point)
//        {
//            var reply = _positionStorage.Move(CurrentPosition, point, CurrentPlayer);
//
//            CheckStoneField(CurrentPosition, reply.Item1);
//
//            CurrentPosition = reply.Item1;
//            if (reply.Item2 != null && reply.Item2.Eated != 0)
//            {
//                _eated[CurrentPlayer] += reply.Item2.Eated;
//                InvokeEatedChanged(EventArgs.Empty);
//            }
//            CurrentPlayer = Util.Opposite(CurrentPlayer);
//        }

        #region private fields

        /// <summary>
        ///     Storage of positions with relations between them
        /// </summary>
        private readonly IPositionStorage _positionStorage;

        /// <summary>
        ///     Information about eated stones
        /// </summary>
        private readonly Dictionary<string, double> _score;

        private IPlayer _currentPlayer;
        private readonly IRules _rules;
        private IPosition _currentPosition;
        public IPlayerProvider PlayerProvider { get; }

        #endregion
    }
}