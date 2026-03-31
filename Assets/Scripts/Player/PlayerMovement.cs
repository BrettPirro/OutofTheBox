using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace Box.Player 
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]

    public class PlayerMovement : MonoBehaviour
    {
        [Range(0, 100)] public float speed = 7f;
        Rigidbody2D rb;
        public Transform body;
        Animator animator;
        bool stopMoving = false;
        [SerializeField]Light2D light;
        

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }


        public void updatePlayerVelocity(Vector2 playerInput)
        {
            if (stopMoving) { rb.linearVelocity = Vector2.zero; return; }
            rb.linearVelocity = playerInput * speed;
            if (playerInput.x != 0) { body.transform.localScale = new Vector2(-(Mathf.Sign(playerInput.x)), 1); }
            animator.SetBool("Walking",playerInput!=Vector2.zero);

        }


        public void toggleMovement()
        {
            stopMoving = (!stopMoving);
        }

        public void increaseLight(float increase) 
        {
            light.pointLightOuterRadius += increase;
        }




    }

}