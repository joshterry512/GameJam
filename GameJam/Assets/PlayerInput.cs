using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerFinal))]
public class PlayerInput : MonoBehaviour {
	PlayerFinal player;
	public float attackRate = 2f;
    float nextAttackTime = 0f;
	void Start () {
		player = GetComponent<PlayerFinal> ();
	
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		//player.SetDirectionalInput (directionalInput);
       

		if (Input.GetKeyDown (KeyCode.Z)) {
			//player.OnJumpInputDown (1);
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
		//	player.OnJumpInputUp();
		}
		if(Time.time >= nextAttackTime) {
			if (Input.GetKeyDown(KeyCode.X))
			{
			    player.Attack();
				nextAttackTime = Time.time + 1f / attackRate;
			}
		}
	}
	

	
}
