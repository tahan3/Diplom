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
        
        private void Awake()
        {
            DialogWindow window = Instantiate(dialogWindow, transform.parent);
            window.Init(desctiption, LeaveRoom);
            window.gameObject.SetActive(false);

            GetComponent<Button>().onClick.AddListener(() => window.gameObject.SetActive(true));
        }
    }
}
