using System;
using UnityEngine;

namespace Managers
{
    public class VibroManager : MonoBehaviour
    {
        public static bool status;

        private void Awake()
        {
            status = Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_NAMES.MuteVibroName.ToString(), 0));
        }
    }
}