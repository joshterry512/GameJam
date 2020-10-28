using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0) {
            if(player.velocity.x < 0) {
                direction = 1;
            }
            else if(player.velocity.x > 0) {
                direction = 2;
            }
        }
        else {
            if(dashTime <= 0) {
                direction = 0;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
            }
            else {
                dashTime -= Time.deltaTime;
                if(direction == 1 && Input.GetKeyDown(KeyCode.C)) {
                    rigidBody.velocity = Vector2.left * dashSpeed;
                }
                else if(direction == 2 && Input.GetKeyDown(KeyCode.C)) {
                    rigidBody.velocity = Vector2.right * dashSpeed;
                }
            }

        }
    }
}
