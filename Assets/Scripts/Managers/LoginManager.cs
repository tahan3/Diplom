using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Managers
{
    public class LoginManager
    {
        public Action OnLogin;
        public Action<GetAccountInfoResult> OnGetAccountInfoSuccess;
        
        public void Login()
        {
            var request = new LoginWithCustomIDRequest()
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
            };
        
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);
        }

        private void OnSuccessLogin(LoginResult result)
        {
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest {PlayFabId = result.PlayFabId},
                OnGetAccountInfoResult, OnError);

            OnLogin?.Invoke();
        }
        
        private void OnGetAccountInfoResult(GetAccountInfoResult result)
        {
            OnGetAccountInfoSuccess?.Invoke(result);
            //playerStatistics.nickname = result.AccountInfo.TitleInfo.DisplayName;
        }
        
        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    }
}
