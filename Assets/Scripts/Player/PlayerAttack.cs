using UnityEngine;

namespace Box.Player 
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttack : Attack
    {
        Animator playerAnimator;
        [SerializeField] GameObject Arrow;
        bool attackInProgress=false;

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
        }

        override public void MeleeAttack() 
        {
            if (attackInProgress) { return; }
            playerAnimator.SetTrigger("Attack");
            Debug.Log("Attack");
            attackInProgress = true;
        }

        override public void RangeAttack() 
        {
            if (attackInProgress) { return; }
            playerAnimator.SetTrigger("Bow");
            Debug.Log("Bow");
            attackInProgress = true;

        }

        public void AttackDone() 
        {
            attackInProgress = false;

        }



        public void SpawnArrow() 
        {
        
        }

    }
}
