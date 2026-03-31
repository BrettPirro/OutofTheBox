using UnityEngine;
using System.Collections.Generic;

public class DestructableItem : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMelee") { spawnRandomItem(); Destroy(this.transform.parent.gameObject); Debug.Log("Worked");  }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerRange") { spawnRandomItem(); Destroy(this.transform.parent.gameObject); Debug.Log("Worked");  }
        
    }

    private void spawnRandomItem() 
    {
        if (Random.Range(1f, 10f) > 7) 
        {
            Instantiate(items[Random.Range(0, items.Count)],this.transform.position,this.transform.rotation);
        }
    }


}

