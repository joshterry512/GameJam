using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {
	private Rigidbody2D rigidBody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
	
	 private int direction;
	Player player;
	public Animator animator;
	void Start () {
		player = GetComponent<Player> ();
		rigidBody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Dash();
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Z)) {
			player.OnJumpInputDown (1);
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			player.OnJumpInputUp ();
		}
		if (Input.GetKeyDown(KeyCode.X))
        {
              Attack();
        }

	}
	void Attack() {
        // play an attack animation
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        // damage the enemies
    }

	void Dash() {
		if(direction == 0) {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = 1;
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = 2;
            }
        }
        else {
            if(dashTime <= 0) {
				animator.SetBool("isDash", false);
                direction = 0;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
            }
            else {
                dashTime -= Time.deltaTime;
                if(direction == 1 && Input.GetKeyDown(KeyCode.C)) {
					animator.SetBool("isDash", true);
                    rigidBody.velocity = Vector2.left * dashSpeed;
                }
                else if(direction == 2 && Input.GetKeyDown(KeyCode.C)) {
					animator.SetBool("isDash", true);
                    rigidBody.velocity = Vector2.right * dashSpeed;
                }
            }
        }
	}
}
