using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private Rigidbody rb;
    private bool used;
    private KeyInput player;
    int timer;
    private void OnCollisionEnter(Collision collision)
    {
        if (!used)
        {
            if (collision.gameObject.tag == "Player")
            {
                player = collision.gameObject.GetComponent<KeyInput>();
                used = true;
            }
            else if (collision.gameObject.tag == "Dummy")
            {
                player = collision.gameObject.GetComponent<CollisionCopyMovement>().playerObj.GetComponent<KeyInput>();
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
                player = other.gameObject.GetComponent<KeyInput>();
                used = true;
            }
            else if (other.gameObject.tag == "Dummy")
            {
                player = other.gameObject.GetComponent<CollisionCopyMovement>().playerObj.GetComponent<KeyInput>();
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
            if (timer<150)
            {
                player.speed = 40.0f;
            }
            else
            {
                player.speed = 18.0f;
            }
        }
    }

}
