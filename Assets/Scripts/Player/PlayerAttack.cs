using UnityEngine;

namespace Box.Player 
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttack : Attack
    {
        Animator playerAnimator;
        [SerializeField] GameObject Arrow;
        bool attackInProgress=false;
        [SerializeField] Transform arrowSpawn;
        public int attackAmount = 1;
        public int rangeAmount = 1;

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
            var currentRot = Quaternion.EulerRotation(new Vector3(90, 0, 0));

            if (transform.localScale.x == 1) { currentRot = transform.rotation; }


            GameObject b = Instantiate(Arrow, arrowSpawn.transform.position, currentRot);
            b.GetComponent<Rigidbody2D>().AddForce((Vector2.left*GetComponent<PlayerMovement>().body.transform.localScale.x) * 1f, ForceMode2D.Impulse);
            Destroy(b, 2f);
        }

    }
}
