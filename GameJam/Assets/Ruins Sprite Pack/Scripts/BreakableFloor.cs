using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour {

    [Tooltip("This is public just so you can test it out in the inspector, idealy some game system will change this once.")]
    public bool trigger = false;

    public GameObject breakableFloor;
    public GameObject breakableFloorCracked;
    public GameObject contactParticles;
    public GameObject fallingBrickFragments;

    [Header("How long until it colapses")]
    public float cracksTimer;
    WaitForSeconds waitCracksTimer;

    Collider2D collider;

    private void Start()
    {
        waitCracksTimer = new WaitForSeconds(cracksTimer);
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (trigger == true)
        {
            StartCoroutine(Trigger());
            trigger = false;
        }
    }

    IEnumerator Trigger()
    {
        breakableFloor.SetActive(false);
        breakableFloorCracked.SetActive(true);
        contactParticles.SetActive(true);

        yield return waitCracksTimer;

        breakableFloorCracked.SetActive(false);
        if (collider != null)
            collider.enabled = false;

        fallingBrickFragments.SetActive(true);
    }

    //Trigger the object's action when the player enters the collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && !trigger)
        {
            trigger = true;
        }
    }




}
