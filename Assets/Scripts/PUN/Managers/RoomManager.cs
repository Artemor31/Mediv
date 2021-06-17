// using Photon.Pun;
// using Photon.Realtime;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace PUN
// {
//     public class RoomManager : MonoBehaviourPunCallbacks
//     {
//         // public static RoomManager Instance;
//         //
//         // [SerializeField] private GameObject playerManager;
//         //
//         // private void Awake()
//         // {
//         //     if (Instance)
//         //     {
//         //         Destroy(gameObject);
//         //         return;
//         //     }
//         //     DontDestroyOnLoad(gameObject);
//         //     Instance = this;
//         // }
//
//         public override void OnEnable()
//         {
//             base.OnEnable();
//             SceneManager.sceneLoaded += OnSceneLoaded;
//         }
//
//         public override void OnDisable()
//         {
//             base.OnDisable();
//             SceneManager.sceneLoaded -= OnSceneLoaded;
//         }
//
//         void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
//         {
//   //          if(scene.buildIndex == 3)
//    //             PhotonNetwork.Instantiate(playerManager.name, Vector3.zero, Quaternion.identity);
//         }
//
//         public override void OnLeftRoom()
//         {
//             // current player leaves room
//             SceneManager.LoadScene(0);
//         }
//
//         public override void OnPlayerEnteredRoom(Player newPlayer)
//         {
//             Debug.LogFormat("Player {0} enter the room!", newPlayer.NickName);
//         }
//
//         public override void OnPlayerLeftRoom(Player otherPlayer)
//         {
//             Debug.LogFormat("Player {0} leaves the room!", otherPlayer.NickName);
//         }
//
//         public void LeaveRoom()
//         {
//             PhotonNetwork.LeaveRoom();
//         }
//     }
// }
