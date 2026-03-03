using UnityEngine;


namespace Box.Player 
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField][Range(0, 100)] float speed = 7f;
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        public void updatePlayerVelocity(Vector2 playerInput)
        {
            rb.linearVelocity = playerInput * speed;
        }




    }

}