using System.Collections.Generic;
using UnityEngine;

namespace Notifications
{
    [CreateAssetMenu(fileName = "NotificationsData", menuName = "ScriptableObjects/NotificationsData", order = 2)]
    public class NotificationsData : ScriptableObject
    {
        [SerializeField] private List<NotificationItem> notificationItems;

        public NotificationItem GetRandomNotification()
        {
            return notificationItems[Random.Range(0, notificationItems.Count)];
        }
    }
}
