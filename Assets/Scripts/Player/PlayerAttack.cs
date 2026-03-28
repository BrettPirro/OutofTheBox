using UnityEngine;

namespace Box.Player 
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttack : MonoBehaviour
    {
        Animator playerAnimator;

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
        }

        public void playerMeleeAttack() 
        {
            playerAnimator.SetTrigger("Attack");
            Debug.Log("Attack");
        }



    }
}
