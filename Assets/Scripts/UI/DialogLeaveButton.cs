using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

////////ЗАПИСКА

namespace UI
{
    public class DialogLeaveButton : LeaveButton
    {
        [SerializeField] private DialogWindow dialogWindow;

        private readonly string desctiption = "Do you want to exit the game?";

        private Button button;
        
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(InitDialogButton);
        }

        private void InitDialogButton()
        {
            DialogWindowsManager.Instance.CreateDialogWindow(DialogWindowType.OptionalWindow, desctiption, LeaveRoom);
        }
    }
}
