using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;
using Valker.PlayOnLan.GoPlugin.Annotations;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    ///     Implements Go rules
    /// </summary>
    public sealed class Engine : IEngine
    {
        /// <summary>
        ///     Создать объект "движка"
        /// </summary>
        /// <param name="positionStorage"></param>
        /// <param name="playerProvider"></param>
        /// <param name="rules"></param>
        /// <param name="scoreStorageFactory"></param>
        public Engine(
            IPositionStorage positionStorage,
            IPlayerProvider playerProvider,
            IRules rules)
        {
            PlayerProvider = playerProvider;
            _rules = rules;
            _positionStorage = positionStorage;
            CurrentPosition = _positionStorage.Initial;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
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
                OnPropertyChanged("CurrentPlayer");
            }
        }

        public double GetScore(IPlayer player)
        {
            return CurrentPosition.GetScore(player);
        }

        public IPlayer CurrentPlayer => _currentPosition.CurrentPlayer;

        #endregion

        #region events

        public event EventHandler<CellChangedEventArgs> CellChanged;

        public event EventHandler<ScoreChangedEventArgs> ScoreChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region methods

        public void Move(IMove move)
        {
            IPosition oldPosition = CurrentPosition;
            IMoveConsequences moveConsequences = move.Perform(CurrentPosition, PlayerProvider);
            _rules.IsAcceptable(oldPosition, moveConsequences, _positionStorage);

            _positionStorage.AddChildPosition(oldPosition, moveConsequences.Position);

            foreach (Tuple<ICoordinates, ICell> tuple in oldPosition.CompareStoneField(moveConsequences.Position))
            {
                OnCellChanged(new CellChangedEventArgs(tuple.Item1, tuple.Item2));
            }

            foreach(Tuple<IPlayer, double> tuple in oldPosition.CompareScore(moveConsequences.Position))
            {
                OnScoreChanged(new ScoreChangedEventArgs(tuple.Item1, tuple.Item2));
            }

            CurrentPosition = moveConsequences.Position;
        }

        #endregion

        #region private fields

        /// <summary>
        ///     Storage of positions with relations between them
        /// </summary>
        private readonly IPositionStorage _positionStorage;

        private readonly IRules _rules;
        private IPosition _currentPosition;
        public IPlayerProvider PlayerProvider { get; }

        #endregion

        private void OnCellChanged(CellChangedEventArgs e)
        {
            CellChanged?.Invoke(this, e);
        }

        private void OnScoreChanged(ScoreChangedEventArgs e)
        {
            ScoreChanged?.Invoke(this, e);
        }
    }
}