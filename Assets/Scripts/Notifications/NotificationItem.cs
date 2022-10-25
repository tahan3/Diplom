using System;
using UnityEngine;

namespace Notifications
{
    [Serializable]
    public struct NotificationItem
    {
        public Sprite icon;
        public string title;
        public string content;
    }
}
