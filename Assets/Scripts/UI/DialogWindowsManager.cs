using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class DialogWindowsManager : MonoBehaviour
    {
        [SerializeField] private DialogWindow warningWindowPrefab;
        [SerializeField] private DialogWindow optionalWindowPrefab;
        
        public static DialogWindowsManager Instance { get; private set; }

        private void Awake()
        {
            #region Singleton
            if (Instance == null)
            {
                Instance = this;
            } else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            #endregion
        }

        public void CreateDialogWindow(DialogWindowType type, string context, UnityAction callback = null)
        {
            DialogWindow window = Instantiate(GetWindowByType(type), transform);
            window.Init(context, callback);
        }

        private DialogWindow GetWindowByType(DialogWindowType type)
        {
            switch (type)
            {
                case DialogWindowType.OptionalWindow:
                    return optionalWindowPrefab;
                case DialogWindowType.WarningWindow:
                    return warningWindowPrefab;
            }

            return null;
        }
    }
}
