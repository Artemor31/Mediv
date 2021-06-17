using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace PUN
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text logText;

        void Start()
        {
            PhotonNetwork.NickName = "Player" + Random.Range(228, 323);
            PhotonNetwork.GameVersion = "V.0.1";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Log("Connected to master");
        }

        public void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions {MaxPlayers = 2};
            PhotonNetwork.CreateRoom(null, roomOptions);
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            Log("\n" + PhotonNetwork.NickName + " joined room!");
            PhotonNetwork.LoadLevel("SampleScene");
        }

        private void Log(string message)
        {
            logText.text += "\n" + message;
        }
    }
}