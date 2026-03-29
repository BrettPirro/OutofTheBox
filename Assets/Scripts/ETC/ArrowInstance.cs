using UnityEngine;

public class ArrowInstance : MonoBehaviour
{
    [SerializeField]Transform body;


    private void Start()
    {
        if (GetComponent<Rigidbody2D>().linearVelocityX>0) { body.localScale = new Vector3(-1, 1, 1); }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DestructProp") { Destroy(this.gameObject); }
    }


}
