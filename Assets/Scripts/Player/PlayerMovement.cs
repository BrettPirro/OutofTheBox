using UnityEngine;


namespace Box.Player 
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField][Range(0, 100)] float speed = 7f;
        Rigidbody2D rb;
        [SerializeField] Transform body;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        public void updatePlayerVelocity(Vector2 playerInput)
        {
            rb.linearVelocity = playerInput * speed;
            if (playerInput.x != 0) { body.transform.localScale = new Vector2(playerInput.x, 1); }
        
        }




    }

}