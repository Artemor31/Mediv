using Photon.Pun;
using UnityEngine;

namespace PUN
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
    
        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (_photonView.IsMine)
            {
                CreateController();
            }
        }

        void CreateController()
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,2,0), Quaternion.identity);
        }
    
    
    
    }
}
