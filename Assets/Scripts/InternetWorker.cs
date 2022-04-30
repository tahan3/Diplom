using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetWorker
{
    public static bool InternetConnectionCheck()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}