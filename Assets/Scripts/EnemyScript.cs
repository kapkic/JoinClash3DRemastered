using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject[] enemyArr;
    private GameObject enemyObj;
    private GameObject dummyObj;

    private Rigidbody rb;

    private bool collidedAlly, alive=true;

    private Vector3 dummyPos;
    private Vector3 myPos;
    private Vector3 myDir;
	
	private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
		anim=GetComponentInChildren<Animator>();

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
		if (collision.gameObject.name == "Saw")
        {
			setDie();
        }
		if (collision.gameObject.name == "Block")
        {
			setDie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Dummy")
        {
            GetComponent<BoxCollider>().enabled = false;
            collidedAlly = true;
            dummyObj = other.gameObject;
			
			Invoke("setDieAnim", 1);
        }
    }
	
	void setDie()
	{
		//lossTextObject.SetActive(true);
		Destroy(gameObject);
		//if all dies
		//setGameOver();
	}
	
	void setDieAnim()
	{
		alive=false;
		anim.SetTrigger("dead");
		Invoke("setDie", 2);
	}

    private void FixedUpdate()
    {
		if (alive)
		{
        if (collidedAlly == true)
        {
            dummyPos = dummyObj.GetComponent<Rigidbody>().position;
            myPos = rb.position;
            myDir = dummyPos - myPos;
            myDir.Normalize();
            rb.velocity = myDir * 2;
        }
		
		if(rb.velocity.magnitude>1)   
			anim.SetBool("isRunning", true);
		else if(rb.velocity.magnitude<=1)   
			anim.SetBool("isRunning", false);
    }
	}
}