using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private Rigidbody rb;
    private bool used;
    private PlayerController player;
    int timer;
    private void OnCollisionEnter(Collision collision)
    {
        if (!used)
        {
            if (collision.gameObject.tag == "Player")
            {
                player = collision.gameObject.GetComponent<PlayerController>();
                used = true;
            }
            else if (collision.gameObject.tag == "Ally")
            {
                player = collision.gameObject.GetComponent<CollisionCopyMovement>().playerObj.GetComponent<PlayerController>();
                used = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject.GetComponent<PlayerController>();
                used = true;
            }
            else if (other.gameObject.tag == "Ally")
            {
                player = other.gameObject.GetComponent<CollisionCopyMovement>().playerObj.GetComponent<PlayerController>();
                used = true;
            }
        }
    }


    void Start()
    {

    }

    void FixedUpdate()
    {
        if (used)
        {
            timer++;
            if (timer < 150)
            {
                player.speed = 28.0f;
            }
            else
            {
                player.speed = 18.0f;
            }
        }
    }

}
