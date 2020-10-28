﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnJumpInputDown (1);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
	}
<<<<<<< HEAD:GameJam/Assets/MatthewScripts/PlayerInput.cs
    void Attack() {
        // play an attack 
        animator.SetTrigger("Attack");
		// Detect enemies in range of attack
		// damage the enemies
	}
=======
>>>>>>> parent of fcbfd4a... Level1.unity:GameJam/Assets/Ruins Sprite Pack/Scenes/Playable Demo Scene/2D Character Controller/Scripts/PlayerInput.cs
}
