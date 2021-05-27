using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCopyMovement : MonoBehaviour
{
    public GameObject playerObj;

    public Material blue;
    public Material empty;

    private Rigidbody rb;

	public GameObject[] dummyArr;
	private GameObject dummyObj;
    private GameObject enemyObj;

    private bool collidedPlayer;
    private bool collidedDummy;
    private bool collidedEnemy;
	private bool jump;
	private bool elem=true, alive=true;
	private bool active=false;
	
	public bool taken=false;
	public bool playerActivated=false;
	
    private Vector3 enemyPos;
    private Vector3 dummyPos;
    private Vector3 dummyDir;
	
	private Animator anim;
	
	public Transform boss;
	public BossHP hp2;
	
	int moveSpeed = 4;
	int maxDist = 3;
	int minDist = 2;
	

	public bool isTaken()
	{
	return taken;
	}
	
	public void setTaken()
	{
	taken=true;
	}
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collidedPlayer = false;
        collidedDummy = false;
        collidedEnemy = false;

		dummyArr = GameObject.FindGameObjectsWithTag("Dummy");

		anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.CompareTag("Saw"))
		{
			alive = false;
			anim.SetTrigger("dead");
			Invoke("setDie", 2);
		}
	}

    private void FixedUpdate()
    {
		if (alive)
		{
		if(rb.velocity.magnitude>1)   
			anim.SetBool("isRunning", true);
		else if(rb.velocity.magnitude<=1)   
			anim.SetBool("isRunning", false);
			
		if (jump)
		{
			anim.SetTrigger("jumping");
			jump=false;
		}
		
        if (collidedEnemy == true)
        {
			//if (!other.gameObject.GetComponent<EnemyScript>().isTaken())
			//{
			//other.gameObject.GetComponent<EnemyScript>().setTaken();
			//}
			
        }

        if (collidedPlayer == true)
        {
            rb.velocity = playerObj.GetComponent<Rigidbody>().velocity;
            GetComponentInChildren<SkinnedMeshRenderer>().material = blue;
			playerActivated=true;
        }
        else if (collidedDummy == true)
        {	Color a,b;
			if(dummyObj != null)
                {
					if (dummyObj.GetComponent<CollisionCopyMovement>().playerActivated)
					{
						rb.velocity = playerObj.GetComponent<Rigidbody>().velocity;
						//dummyObj.GetComponent<CollisionCopyMovement>().setplayerActivated();
						setplayerActivated();
					}
					else
					{
						rb.velocity = dummyObj.GetComponent<Rigidbody>().velocity;
					}

					a = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
					b = dummyObj.GetComponentInChildren<SkinnedMeshRenderer>().material.color;

					if (!a.Equals(b))
					{
						GetComponentInChildren<SkinnedMeshRenderer>().material = blue;
					}
				}
			
			
        }
		}
    }
	
	public void setplayerActivated()
	{
		playerActivated=true;
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
				
				dummyObj = other.gameObject;
				if(dummyObj != null)
                {
					collidedDummy = true;
					setJoin();
				}
					
				
					
            }
        }

        if (other.gameObject.CompareTag("Coin"))
		{
			other.gameObject.SetActive(false);
			playerObj.GetComponent<PlayerController>().SetCountText();
		}
		else if (other.gameObject.CompareTag("Speed"))
		{
			other.gameObject.SetActive(false);
			//speed += 10; increase total speed
			SoundManager.playSpeedSound();

		}
		if (other.gameObject.CompareTag("BossStep"))
        {
			setFight();
        }
		
		if (other.gameObject.CompareTag("Block"))
        {
			alive=false;
			anim.SetTrigger("dead");
			Invoke("setDie", 2);
        }
		if (other.gameObject.CompareTag("Enemy"))
        {
			//if (!other.gameObject.GetComponent<EnemyScript>().isTaken())
			Invoke("setDieAnim", 1);

        }
		if (other.gameObject.CompareTag("Dummy"))
        {
			setJoin();		
        }
		
		if (other.gameObject.CompareTag("Ramp"))
        {
			jump=true;
        }
		
	}
	
	void setDieAnim(){
			alive=false;
			anim.SetTrigger("dead");
			Invoke("setDie", 2);
	}
	void setDie()
	{
		//lossTextObject.SetActive(true);
		//wait 3 seconds
		SoundManager.playPopSound();
		Destroy(gameObject);
		dummyArr = GameObject.FindGameObjectsWithTag("Dummy");
		//if all dies
		//setGameOver();
	}
	
	private void setJoin()
	{
			SoundManager.playJoinSound();
	}
	
	void initFightOnce()
    {
		if (elem)
		{
        anim.SetBool("isFighting",true);
		elem=false;
		}
    }
	
	void setFight()
	{
		active=true;
	}

	
	void Update()
    {
	//	Debug.Log(Vector3.Distance(transform.position, Player.position));
        if (active){
		transform.LookAt(boss);
			if (hp2.isActive())
			{
				if (Vector3.Distance(transform.position, boss.position) >= minDist)
				 {
					 transform.position += transform.forward * moveSpeed * Time.deltaTime;

					 if (Vector3.Distance(transform.position, boss.position) <= maxDist)
					 {
						 initFightOnce();
						 hp2.takeDamage(0.1f);
						 //
					 }
				 }
			}
			else if (!elem)
			{
				anim.SetBool("isDancing",true);	
			}
		}
    }
	
}
