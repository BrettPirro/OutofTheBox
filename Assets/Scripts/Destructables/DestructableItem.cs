using UnityEngine;
using System.Collections.Generic;

public class DestructableItem : MonoBehaviour
{
    [SerializeField] List<Items> items = new List<Items>();   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMelee") { Destroy(this.transform.parent.gameObject); Debug.Log("Worked"); }
    }


}

public class Items 
{
    public GameObject prefab;
    public float dropPercent;
}