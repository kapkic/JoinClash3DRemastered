using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCopyMovement : MonoBehaviour
{
    public GameObject playerObj;

    private Rigidbody rb;

    private float movSpeed;
    private bool collided;
    private Vector3 movDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collided = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "Dummy")
        {
            collided = true;
        }
    }

    private void FixedUpdate()
    {
        if(collided == true)
        {
            //movSpeed = playerObj.GetComponent<Rigidbody>().velocity.magnitude;
            //movDir = playerObj.GetComponent<Rigidbody>().velocity.normalized;
            //playerObj.GetComponent<Rigidbody>().
            rb.velocity = playerObj.GetComponent<Rigidbody>().velocity;
        }
    }
}
