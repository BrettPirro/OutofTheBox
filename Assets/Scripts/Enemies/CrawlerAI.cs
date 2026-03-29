using UnityEngine;
using Box.Player;

[RequireComponent(typeof(PlayerIdentifier))]
public class CrawlerAI : MonoBehaviour
{

    Health health;
    PlayerAttack player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerAttack>();
        health = GetComponent<Health>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMelee") { health.DealDamage(player.attackAmount, Mathf.RoundToInt(player.gameObject.GetComponent<PlayerMovement>().body.transform.localScale.x)); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerRange") { health.DealDamage(player.rangeAmount, 0); }

    }









}
