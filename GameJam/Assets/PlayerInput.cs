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
		if(Time.time >= nextAttackTime) {
			if (Input.GetKeyDown(KeyCode.X))
			{
			    player.Attack();
				nextAttackTime = Time.time + 1f / attackRate;
			}
		}
	}
	

	
}
