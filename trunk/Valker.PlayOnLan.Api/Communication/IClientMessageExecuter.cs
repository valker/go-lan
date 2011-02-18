namespace Valker.PlayOnLan.Api.Communication
{
    public interface IClientMessageExecuter
    {
        void UpdateSupportedGames(object sender, string[] games);
        void ShowMessage(string text);
    }
}