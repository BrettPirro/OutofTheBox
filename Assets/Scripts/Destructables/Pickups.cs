using UnityEngine;

public class Pickups : MonoBehaviour
{
    [Header("buffs")]
    [SerializeField] float speedIncrease;
    [SerializeField] float lightIncrease;
    [SerializeField] float heartIncrease;
    [SerializeField] float damageIncrease;

    [Header("Debuffs")]
    [SerializeField] float takeDamage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") { return; }


    }




}
