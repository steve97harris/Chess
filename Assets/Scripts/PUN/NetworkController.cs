using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace.PUN
{
    public class NetworkController : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            // Connect to photon master servers
            PhotonNetwork.ConnectUsingSettings();
        }

        #region Mono Methods

        public override void OnConnectedToMaster()
        {
            Debug.LogError($"Connected to server; {PhotonNetwork.CloudRegion}");
        }

        #endregion
        
    }
}