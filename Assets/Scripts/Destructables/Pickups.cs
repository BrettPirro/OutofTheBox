using Box.Player;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [Header("buffs")]
    [SerializeField] float speedIncrease;
    [SerializeField] float lightIncrease;
    [SerializeField] int heartIncrease;
    [SerializeField] int damageIncrease;
    [SerializeField] bool isPotion = false;
    [SerializeField] bool isBox = false;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") { return; }
        var player = GameObject.FindWithTag("Player");
        if (isBox) 
        {
            player.GetComponent<PlayerInput>().AddBox(1);
            Destroy(this.gameObject);
            return;
        }
        else if (isPotion) 
        {
            player.GetComponent<PlayerInput>().AddPotions(1);
            Destroy(this.gameObject);
            return;

        }

        var move= player.GetComponent<PlayerMovement>();
        var attack = player.GetComponent<PlayerAttack>();
        var health = player.GetComponent<Health>();


        move.speed += speedIncrease;
        
        if (heartIncrease != 0) { health.IncreaseHealth(heartIncrease); Destroy(this.gameObject); return; }
        attack.attackAmount += damageIncrease;
        attack.rangeAmount += damageIncrease;
        move.increaseLight(lightIncrease);
        Destroy(this.gameObject);

    }




}
