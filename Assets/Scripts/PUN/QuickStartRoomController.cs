using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace.PUN
{
    public class QuickStartRoomController : MonoBehaviourPunCallbacks
    {
        public int multiplayerSceneIndex;

        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Room Joined");
            InitiateGame();
        }

        private void InitiateGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Starting Game");
                PhotonNetwork.LoadLevel(multiplayerSceneIndex);
            }
        }
    }
}