using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class InternetMenuItem : MenuItem
{
    protected override void Show(Action callback = null)
    {
        if (InternetWorker.InternetConnectionCheck())
        {
            base.Show(callback);
        }
        else
        {
            DialogWindowsManager.Instance.CreateDialogWindow(DialogWindowType.WarningWindow, "No Internet connection!");
        }
    }
}
