using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    SpriteRenderer renderer;
    Animator animator;
    public Transform enemyAttackPoint;
	public float enemyAttackRange = 0.5f;
    public float aiPeripheral = 20f;

    public int enemyAttackDamage = 30;
    public LayerMask playerLayers;
    public float enemyAttackRate = 2.3f;

    public float speed;

    bool aiEnabled;
    private Transform target;
    float nextAttackTime = 0f;
    bool receivedDamage = false;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        aiEnabled = false;
    }

    void Update() {
        aiEnabled = Physics2D.OverlapCircleAll(enemyAttackPoint.position, aiPeripheral, playerLayers).Length != 0; 

        if(Vector2.Distance(transform.position, target.position) > 2 && aiEnabled) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        if(Time.time >= nextAttackTime) {
             StartCoroutine(AttackRoutine());
             nextAttackTime = Time.time + 1f / enemyAttackRate;
        }
       
    }
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        // play hurt animation or make it flash red
        ReceiveDamage(damage);
        if(currentHealth <= 0) {
            renderer.color = Color.red;
            Die();
        }
    }
    IEnumerator AttackRoutine ()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackRange, playerLayers); // finds if the player is within the attack range
        yield return new WaitForSeconds(0.3f); // wait a bit
        player = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackRange, playerLayers); // checks if the player is still in attack range
        if(player.Length != 0) { // so the player is still in attack range
            animator.SetTrigger("Attack"); // does the attack animation
            foreach(Collider2D p in player) {
                p.GetComponent<PlayerFinal>().ReceiveDamage(enemyAttackDamage);
            }
        } 
    } 
    void OnDrawGizmosSelected() {

        if(enemyAttackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(enemyAttackPoint.position, enemyAttackRange);
    }
    void Die() {
        Debug.Log("Enemy died");
     
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
        Destroy(gameObject);
        // die animation or disable the enemy
    }

     //Tweak: adding a damage function just to showcase the functionality of the objects that causes damage
    //The character controller doesn't have a health, so the damage value is just ignored here
    
    void ReceiveDamage(float value)
    {
        if (!receivedDamage)
            StartCoroutine(DamageRoutine());
    }
    WaitForSeconds wait = new WaitForSeconds(0.1f);
    IEnumerator DamageRoutine() 
    {
        receivedDamage = true;

        int iteration = 0;
        while (iteration <= 3)
        {
            iteration++;

            if (iteration % 2 == 0)
            {
                renderer.color = Color.red;
            }
            else
            {
                renderer.color = Color.white;
            }

            yield return wait;
        }

        receivedDamage = false;
        renderer.color = Color.white;
        yield return null;
    }
}
