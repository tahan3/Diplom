using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class NicknameChanger : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(() => ValidationCheck(inputField.text));
        }

        private void ValidationCheck(string newName)
        {
            if (newName.Length >= 3)
            {
                ChangeNickname(newName);
            }
            else
            {
                DialogWindowsManager.Instance.CreateDialogWindow(DialogWindowType.WarningWindow, "Invalid nickname");
            }
        }
        
        private void ChangeNickname(string nickname)
        {
            PlayFabManager.Instance.OnNicknameChange += OnNicknameChange;

            void OnNicknameChange()
            {
                PlayFabManager.Instance.OnNicknameChange -= OnNicknameChange;

                DialogWindowsManager.Instance.CreateDialogWindow(DialogWindowType.WarningWindow,
                    "The nickname was changed to " + nickname);
            }
            
            PlayFabManager.Instance.UpdatePlayersName(nickname);
        }
    }
}
