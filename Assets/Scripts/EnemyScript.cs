using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject[] enemyArr;
    private GameObject enemyObj;
    private GameObject dummyObj;

    private Rigidbody rb;

    private bool collidedAlly;

    private Vector3 dummyPos;
    private Vector3 myPos;
    private Vector3 myDir;

    // Start is called before the first frame update
    void Start()
    {
        enemyArr = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject x in enemyArr)
        {
            Physics.IgnoreCollision(x.GetComponent<Collider>(), GetComponent<Collider>());
        }
        rb = GetComponent<Rigidbody>();
        collidedAlly = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            enemyObj = collision.gameObject;
            Physics.IgnoreCollision(enemyObj.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Dummy")
        {
            GetComponent<BoxCollider>().enabled = false;
            collidedAlly = true;
            dummyObj = other.gameObject;
        }
    }

    private void FixedUpdate()
    {
        if (collidedAlly == true)
        {
            dummyPos = dummyObj.GetComponent<Rigidbody>().position;
            myPos = rb.position;
            myDir = dummyPos - myPos;
            myDir.Normalize();
            rb.velocity = myDir * 2;
        }
    }
}