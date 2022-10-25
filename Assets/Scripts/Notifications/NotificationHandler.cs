using System;
using System.Collections;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationHandler : MonoBehaviour
{
    private readonly string CHANNEL_ID = "notification1";
    private readonly string CHANNEL_NAME = "notification1name";

    private readonly string NOTIFY_ID_NAME = "notifyID";

    private readonly string FIRST_NOTIFY_LOAD = "firstNotifyLoad";

    public readonly string PRIZE_STATUS_NAME = "prizeStatus";

    private int notifyID = 666;
    private bool firstLoad;

    private void Awake()
    {
        InitializeNotifications();
        LoadPrefs();

        if (!firstLoad)
        {
            CreateNotification("", "", DateTime.Now./*AddMinutes*/AddSeconds(15));
            firstLoad = true;
            SavePrefs();
        }
    }

    private void Start()
    {
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notifyID) ==
            NotificationStatus.Delivered)
        {
            CreateNotification("", "", DateTime.Now.AddDays(1));
        }

        StartCoroutine(NotificationReceivedCheck(2f));
    }

    private IEnumerator NotificationReceivedCheck(float delay)
    {
        while (enabled)
        {
            yield return new WaitForSecondsRealtime(delay);
            SavePrefs();
            CreateNotification("", "", DateTime.Now.AddDays(1));
        }
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SavePrefs();
        }
    }

    private void OnDisable()
    {
        SavePrefs();
    }

    private void SavePrefs()
    {
        PlayerPrefs.SetInt(FIRST_NOTIFY_LOAD, Convert.ToInt32(firstLoad));
    }

    private void LoadPrefs()
    {
        firstLoad = Convert.ToBoolean(PlayerPrefs.GetInt(FIRST_NOTIFY_LOAD, 0));
    }

    private void InitializeNotifications()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = CHANNEL_ID,
            Name = CHANNEL_NAME,
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    private void CreateNotification(string title, string body, DateTime time)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = body;
        notification.FireTime = time;

        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, CHANNEL_ID, notifyID);
    }
}
