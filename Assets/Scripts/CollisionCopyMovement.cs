using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCopyMovement : MonoBehaviour
{
    public GameObject playerObj;

    public Material blue;
    public Material empty;

    private Rigidbody rb;

    private GameObject friendDummyObj;
    private GameObject neutralDummyObj;
    private GameObject enemyObj;

    private bool collidedPlayer;
    private bool collidedFriend;
    private bool collidedEnemy;
    private bool collidedNeutral;
    public bool isFriend;
    public bool isNeutral;

    private Vector3 enemyPos;
    private Vector3 dummyPos;
    private Vector3 dummyDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collidedEnemy = false;
        isFriend = false;
        isNeutral = true;
    }

    private void OnCollisionEnter(Collision other)
    {



    }

    private void FixedUpdate()
    {
        if (collidedEnemy)
        {
            enemyPos = enemyObj.GetComponent<Rigidbody>().position;
            dummyPos = rb.position;
            dummyDir = enemyPos - dummyPos;
            dummyDir.Normalize();
            rb.velocity = dummyDir * 2;
        }

        if (collidedPlayer)
        {
            rb.velocity = playerObj.GetComponent<Rigidbody>().velocity;
            GetComponentInChildren<SkinnedMeshRenderer>().material = blue;
        }

        if (collidedFriend)
        {
            // Color a,b;
            // rb.velocity = friendDummyObj.GetComponent<Rigidbody>().velocity;
            rb.velocity = playerObj.GetComponent<Rigidbody>().velocity;

            //  a = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            //  b = dummyObj.GetComponentInChildren<SkinnedMeshRenderer>().material.color;

            // if (!a.Equals(b))
            //{
            GetComponentInChildren<SkinnedMeshRenderer>().material = blue;
            // }
        }

        if (collidedNeutral)
        {
            rb.velocity = neutralDummyObj.GetComponent<Rigidbody>().velocity;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy")
        {
            collidedEnemy = true;
            collidedPlayer = false;
            collidedFriend = false;
            collidedNeutral = false;
            enemyObj = other.gameObject;
        }

        if (!collidedEnemy)
        {
            if (other.gameObject.name == "Player")
            {
                collidedPlayer = true;
                isFriend = true;
                collidedFriend = false;
                collidedNeutral = false;
            }

            if (collidedPlayer == false && other.gameObject.name == "Dummy" && other.gameObject.GetComponent<CollisionCopyMovement>().isFriend)
            {
                friendDummyObj = other.gameObject;
                collidedFriend = true;
                collidedNeutral = false;
                isFriend = true;
            }

            if (collidedPlayer == false && !collidedFriend && other.gameObject.name == "Dummy" && !other.gameObject.GetComponent<CollisionCopyMovement>().isFriend)
            {
                neutralDummyObj = other.gameObject;
                collidedNeutral = true;
            }
        }

    }
}