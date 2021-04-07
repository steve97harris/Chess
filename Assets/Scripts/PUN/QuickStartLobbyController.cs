using System;
using System.Security.Cryptography;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace.PUN
{
    public class QuickStartLobbyController : MonoBehaviourPunCallbacks
    {
        public Button QuickStartButton;
        public Button QuickCancelButton;

        private const int RoomSize = 2;

        private void Start()
        {
            RegisterListeners();
        }

        #region Mono Methods

        public override void OnConnectedToMaster()
        {
            Debug.LogError("OnConnectedToMaster");
            PhotonNetwork.AutomaticallySyncScene = true;
            QuickStartButton.gameObject.SetActive(true);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError("Join Random Room Failed, creating room...");
            CreateRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError("Create Room Failed, trying again with new room number");
            CreateRoom();
        }

        #endregion

        #region Private Methods

        private void HandleQuickStart()
        {
            QuickStartButton.gameObject.SetActive(false);
            QuickCancelButton.gameObject.SetActive(true);

            var x = PhotonNetwork.JoinRandomRoom(); // first tries to join existing room
            Debug.LogError($"Handle Quick Start; {x}");

        }

        private void HandleQuickCancel()
        {
            Debug.LogError("Handle Quick Cancel");
            QuickStartButton.gameObject.SetActive(true);
            QuickCancelButton.gameObject.SetActive(false);
            PhotonNetwork.LeaveRoom();
        }

        private void CreateRoom()
        {
            var randomNum = Random.Range(0,10000);
            var roomName = $"Room {randomNum}";
            var roomOptions = new RoomOptions() {IsOpen = true, IsVisible = true, MaxPlayers = (byte) RoomSize};
            PhotonNetwork.CreateRoom(roomName, roomOptions);
            Debug.LogError($"Room Created; {roomName}");
        }

        private void RegisterListeners()
        {
            QuickStartButton.onClick.AddListener(HandleQuickStart);
            QuickCancelButton.onClick.AddListener(HandleQuickCancel);
        }
        
        #endregion
    }
}