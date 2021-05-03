using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCopyMovement : MonoBehaviour
{
    public GameObject playerObj;

    private Rigidbody rb;

    private GameObject dummyObj;
    private GameObject enemyObj;
    private bool collidedPlayer;
    private bool collidedDummy;
    private bool collidedEnemy;
    private Vector3 enemyPos;
    private Vector3 dummyPos;
    private Vector3 dummyDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collidedPlayer = false;
        collidedDummy = false;
        collidedEnemy = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            collidedEnemy = true;
            collidedPlayer = false;
            collidedDummy = false;
            enemyObj = collision.gameObject;
        }

        if (collidedEnemy == false && collision.gameObject.name == "Player")
        {
            collidedPlayer = true;
        }

        if (collidedEnemy == false && collidedPlayer == false && collision.gameObject.name == "Dummy")
        {
            dummyObj = collision.gameObject;
            collidedDummy = true;
        }

    }

    private void FixedUpdate()
    {
        if (collidedEnemy == true)
        {
            enemyPos = enemyObj.GetComponent<Rigidbody>().position;
            dummyPos = rb.position;
            dummyDir = enemyPos - dummyPos;
            dummyDir.Normalize();
            rb.velocity = dummyDir * 2;
        }

        if (collidedPlayer == true)
        {
            rb.velocity = playerObj.GetComponent<Rigidbody>().velocity;
        }
        else if (collidedDummy == true)
        {
            rb.velocity = dummyObj.GetComponent<Rigidbody>().velocity;
        }

    }
}
