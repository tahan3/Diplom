using Photon.Pun;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LeaveButton : MonoBehaviourPunCallbacks
    {
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
