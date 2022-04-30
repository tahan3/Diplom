using Network;

namespace UI
{
    public class MultiplayerMenuItem : MenuItem
    {
        protected override void Show()
        {
            if (ServerConnectionManager.Instance.InLobbyStatus)
            {
                base.Show();
            }
        }
    }
}
