using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	public Animator animator;
	
	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
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
        // play an attack 
        animator.SetTrigger("Attack");
		// Detect enemies in range of attack
		// damage the enemies
	}
}
