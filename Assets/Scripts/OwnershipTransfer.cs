using System;
using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace
{
    public class OwnershipTransfer : MonoBehaviourPun
    {
        private void OnMouseDown()
        {
            Debug.LogError("Request Ownership");
            base.photonView.RequestOwnership();
        }
    }
}