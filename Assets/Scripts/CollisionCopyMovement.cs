using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCopyMovement : MonoBehaviour
{
    public GameObject playerObj;

    public Material blue;
    public Material empty;

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
            GetComponentInChildren<SkinnedMeshRenderer>().material = blue;
        }
        else if (collidedDummy == true)
        {
            Color a,b;
            rb.velocity = dummyObj.GetComponent<Rigidbody>().velocity;

            a = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            b = dummyObj.GetComponentInChildren<SkinnedMeshRenderer>().material.color;

            if (!a.Equals(b))
            {
                GetComponentInChildren<SkinnedMeshRenderer>().material = blue;
            }
        }
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.name == "Enemy")
        {
            collidedEnemy = true;
            collidedPlayer = false;
            collidedDummy = false;
            enemyObj = other.gameObject;
        }

        if (collidedEnemy == false)
        {
            if (other.gameObject.name == "Player")
            {
                collidedPlayer = true;
                collidedDummy = false;
                setJoin();
            }

            if (collidedPlayer == false && other.gameObject.name == "Dummy")
            {
                Debug.Log("AAAAAAAAAAA");
                dummyObj = other.gameObject;
                collidedDummy = true;
                setJoin();
            }
        }

        if (other.gameObject.CompareTag("Coin"))
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
	
	private void setJoin()
	{
			SoundManager.playJoinSound();
	}
}
