using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float knockBack;
    int currentHealth;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        currentHealth = maxHealth;
    }


    private void Start()
    {
        if (this.tag == "Player") { FindObjectOfType<PlayerHealthUI>().AddHearts(maxHealth); }
    }


    public void Heal(int heal)
    {
        if (currentHealth + heal > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += heal; }
        if (this.tag == "Player") { FindObjectOfType<PlayerHealthUI>().CorrectHearts(currentHealth); }

    }
    public void DealDamage(int dealt,int dir) 
    {
        KnockBack(dir);
        if (currentHealth - dealt < 0) { currentHealth = 0; }
        else { currentHealth -= dealt; }
        if (this.tag == "Player") { FindObjectOfType<PlayerHealthUI>().CorrectHearts(currentHealth); if (currentHealth == 0) { SceneLoader.current.loadSceneName("GameOver"); } }
        if (this.tag == "Enemy" && currentHealth==0) { Destroy(this.gameObject); }
    }

    private void KnockBack(int dir) 
    {
        rb.AddForce(new Vector2(dir * knockBack, 0),ForceMode2D.Impulse);
    }

    public void IncreaseHealth(int increase) 
    {
   
        maxHealth += increase;
        if (currentHealth + 1 > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += 1; }
        var hearts = FindObjectOfType<PlayerHealthUI>();
        hearts.AddHearts(maxHealth);
        hearts.CorrectHearts(currentHealth); 
    }

    public int currentHealthReturn() 
    {
        return currentHealth;
    }


}
