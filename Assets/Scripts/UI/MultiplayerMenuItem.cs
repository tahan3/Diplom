using System;
using Network;

namespace UI
{
    public class MultiplayerMenuItem : MenuItem
    {
        protected override void Show(Action callback = null)
        {
            if (ServerConnectionManager.Instance.InLobbyStatus)
            {
                base.Show(callback);
            }
        }
    }
}
