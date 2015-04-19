using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IPlayerBase
    {
        string PlayerName { get; }
    }

    public interface IPlayer : IPlayerBase
    {
        IAgentInfo Agent { get; }
        int Order { get; }
    }
}
