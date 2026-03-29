using UnityEngine;
using System.Collections.Generic;

public class DestructableItem : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMelee") { Destroy(this.transform.parent.gameObject); Debug.Log("Worked"); spawnRandomItem(); }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerRange") { Destroy(this.transform.parent.gameObject); Debug.Log("Worked"); spawnRandomItem(); }
        
    }

    private void spawnRandomItem() 
    {
        if (Random.Range(1f, 10f) > 9) 
        {
            Instantiate(items[Random.Range(0, items.Count)]);
        }
    }


}

