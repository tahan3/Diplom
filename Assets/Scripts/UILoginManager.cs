using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILoginManager : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Text messageText;

    public UnityEvent OnLoggin;

    private int minNickNameLenght = 3;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt(PrefsNames.firstLogin.ToString(), 0).Equals(1))
        {
            OnLoggin?.Invoke();
        }
    }

    public void Login()
    {
        if (inputField.text.Length >= minNickNameLenght)
        {
            PlayFabManager.Instance.OnNicknameChange += OnSetNickname;
            PlayFabManager.Instance.UpdatePlayersName(inputField.text);
        }
        else
        {
            SetDesription("Nickname must be more than 2 characters.");
        }
    }

    private void SetDesription(string descriptionText)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = descriptionText;
    }

    private void OnSetNickname()
    {
        PlayFabManager.Instance.OnNicknameChange -= OnSetNickname;
        
        PlayerPrefs.SetInt(PrefsNames.firstLogin.ToString(), 1);
            
        OnLoggin?.Invoke();
    }
}