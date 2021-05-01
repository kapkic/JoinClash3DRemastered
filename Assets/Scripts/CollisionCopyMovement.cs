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
	private int joinCount=0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collided = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
			if (!collided)
			{
            collided = true;
			setJoin();
			}
        }
		
    }
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Coin"))
		{
			other.gameObject.SetActive(false);
			//count++; increase total count
			//SetCountText();
			SoundManager.playCoinSound();
		}
		else if (other.gameObject.CompareTag("Speed"))
		{
			other.gameObject.SetActive(false);
			//speed += 10; increase total speed
			SoundManager.playSpeedSound();

		}
		if (other.gameObject.CompareTag("Boss"))
        {
			//setFight();
        }
		if (other.gameObject.CompareTag("Saw"))
        {
			setDie();
        }
		if (other.gameObject.CompareTag("Enemy"))
        {
			setDie();
        }
		if (other.gameObject.CompareTag("Dummy"))
        {
			setJoin();		
        }
	}
	
	void setDie()
	{
		//lossTextObject.SetActive(true);
		SoundManager.playPopSound();
		//if all dies
		//setGameOver();
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
	
	private void setJoin()
	{
			SoundManager.playJoinSound();
	}
}
