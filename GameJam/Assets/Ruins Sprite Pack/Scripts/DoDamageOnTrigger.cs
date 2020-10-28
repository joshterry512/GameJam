using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageOnTrigger : MonoBehaviour
{
    [SerializeField]
    float damageValue = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.SendMessage("ReceiveDamage", damageValue);
            Debug.Log("Damaged");
        }
    }
}
