using System.Collections;
using Characters;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace PUN.ModifiedScripts
{
    public class PunStats : MonoBehaviourPunCallbacks
    {
        [SerializeField] private AnimatorScheduler animator;
        [SerializeField] private new PhotonView photonView;

        [SerializeField] private float health = 100;
        [SerializeField] private float money = 0;
        
        private bool _dead;
        
    
        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            animator = transform.GetChild(0).gameObject.GetComponent<AnimatorScheduler>();
        }
        
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health -= damage, 0);
            if (health == 0)
            {
                Die();
            }
        
            //// Sending to network taken damage
            if (photonView.IsMine)
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("damage", damage);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
            }
        }
        
        // take info about damage
        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (!photonView.IsMine && Equals(targetPlayer, photonView.Owner))
            {
                TakeDamage((float)changedProps["damage"] );
            }
        }

        private void Die()
        {
            _dead = true;
            GetComponent<Collider>().enabled = false;
            animator.Die();
            StartCoroutine(RemoveAfterDeath());
        }

        private IEnumerator RemoveAfterDeath()
        {
            yield return new WaitForSeconds(5);
            Destroy(this.gameObject);
        }

        public bool IsDead()
        {
            return _dead;
        }
    }
}
