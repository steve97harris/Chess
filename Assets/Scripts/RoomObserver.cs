using System;
using System.IO;
using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace
{
    public class RoomObserver : EventManager
    {
        private const string PhotonPlayerTag = "PhotonPlayer";
        
        public int currentPlayersInGame;

        private Vector3 nextPlayerSpawnPosition = new Vector3(4.21f, 2.09f, -2.19f);
        private Quaternion nextPlayerSpawnRotation = Quaternion.identity;

        private void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            Debug.Log("Creating Player");

            if (currentPlayersInGame >= 2)
            {
                Debug.LogError("Room Full! Create player failed");
                return;
            }

            var photonPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), nextPlayerSpawnPosition, nextPlayerSpawnRotation);
            var photonView = photonPlayer.GetComponent<PhotonView>();
            Debug.LogError("actor nmber: " + photonView.Controller.ActorNumber);
            SpawnMyTeam(photonView, photonPlayer);
            SetDisplay();
        }

        private void SpawnMyTeam(PhotonView photonView, GameObject photonPlayer)
        {
            if (photonView.Controller.ActorNumber == 1)
            {
                SendEventMessage(EventID.SPAWN_WHITE_TEAM);
            }
            else
            {
                var player2Position = new Vector3(4.21f, 2.09f, 10.46f);
                var player2Quat = new Quaternion(0, 180, 0, 0);
                photonPlayer.transform.position = player2Position;
                photonPlayer.transform.rotation = player2Quat;
                SendEventMessage(EventID.SPAWN_BLACK_TEAM);
            }
        }

        private void SetDisplay()
        {
            var players = GameObject.FindGameObjectsWithTag(PhotonPlayerTag);
            for (int i = 0; i < players.Length; i++)
            {
                var photonView = players[i].GetComponent<PhotonView>();
                if (photonView.IsMine)
                {
                    players[i].transform.GetChild(0).GetComponent<Camera>().targetDisplay = 0;
                }
                else
                {
                    players[i].transform.GetChild(0).GetComponent<Camera>().targetDisplay = 1;
                }
            }
        }
    }
}