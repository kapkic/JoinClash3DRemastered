using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCopyMovement : MonoBehaviour
{
    public GameObject playerObj;
    private Rigidbody rb;
    private GameObject enemyObj;
    private bool collidedAlly;
    private bool collidedEnemy;
    private Vector3 enemyPos;
    private Vector3 dummyPos;
    private Vector3 enemyDir;
    private Vector3 dummyDir;
    private Vector3 afterCol = new Vector3(1,2,1);	
	

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collidedAlly = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.name == "Enemy")
        {
            collidedEnemy = true;
            collidedAlly = false;
            enemyObj = collision.gameObject;
        }

        if (collidedEnemy == false && collision.gameObject.name == "Player" || collision.gameObject.tag == "Dummy")
        {
            collidedAlly = true;
			setJoin();
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
        if (collidedEnemy == true)
        {
            enemyObj.GetComponent<BoxCollider>().size = afterCol;
            dummyPos = rb.position;
            enemyPos = enemyObj.GetComponent<Rigidbody>().position;
            dummyDir = enemyPos - dummyPos;
            dummyDir.Normalize();
            enemyDir = dummyPos - enemyPos;
            enemyDir.Normalize();
            rb.velocity = dummyDir * 2;
            enemyObj.GetComponent<Rigidbody>().velocity = enemyDir * 2;
        }

        if (collidedAlly == true)
        {
            rb.velocity = playerObj.GetComponent<Rigidbody>().velocity;
        }

    }
	
	private void setJoin()
	{
			SoundManager.playJoinSound();
	}
}
