using System;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;
using UI;
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
        }
        
        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }

        public IEnumerator ReloginLoop(float delay)
        {
            while (!InternetWorker.InternetConnectionCheck())
            {
                yield return new WaitForSecondsRealtime(delay);

                if (InternetWorker.InternetConnectionCheck())
                {
                    Login();
                }
            }
        }
    }
}
