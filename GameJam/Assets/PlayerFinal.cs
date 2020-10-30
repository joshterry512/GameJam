using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinal : MonoBehaviour
{
    public float speed;
    public float dashForce;
    public float startDashTimer;
    
    public float maxHealth = 100;
    float currentHealth;
    float currentDashTimer;
    
    float dashDirection;
    bool isDashing;
     public int attackDamage = 50;
    Rigidbody2D rigidbody;
    bool facingRight = true;

    bool isGrounded;
    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    public Transform attackPoint;
	public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    
    Animator animator;
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(input * speed, rigidbody.velocity.y);

        if(Input.GetKeyDown(KeyCode.C) && !isGrounded && input != 0) {
            isDashing = true;
            animator.SetBool("isDash", true);
            currentDashTimer = startDashTimer;
            rigidbody.velocity = Vector2.zero;
            dashDirection = (int) input;
        }
        if(isDashing) {
            rigidbody.velocity = transform.right * dashDirection * dashForce;

            currentDashTimer -= Time.deltaTime;

            if(currentDashTimer <= 0) {
                animator.SetBool("isDash", false);
                isDashing = false;
            }
        }
        if(input != 0) {
            animator.SetBool("isRunning", true);
        }
        else {
            animator.SetBool("isRunning", false);
        }
        if(input > 0 && facingRight == false) {
            Flip();
        }
        else if(input < 0 && facingRight == true) {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(Input.GetKeyDown(KeyCode.Z) && isGrounded == true) {
            rigidbody.velocity = Vector2.up * jumpForce;
        }
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        if(isTouchingFront = true && isGrounded == false && input != 0) {
            wallSliding = true;
        }
        else {
             wallSliding = false;
        }
        if(wallSliding) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }
    void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
     void OnDrawGizmosSelected() {
         Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
     }

     public void Attack() {
        // play an attack animation
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<EnemyMovement>().TakeDamage(attackDamage);
        }
        // damage the enemies
    }
     void OnDrawGizmosSelectedAttack() {

        if(attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    bool receivedDamage = false;
    public void ReceiveDamage(int damage)
    {
        if (!receivedDamage)
            currentHealth -= damage;
            if(currentHealth <= 0) {
                Die();
                return;
            }
            StartCoroutine(DamageRoutine());
    }
    // 
    void Die() {
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
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
