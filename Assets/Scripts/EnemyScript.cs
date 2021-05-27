using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject[] enemyArr;
    private GameObject[] dummyArr;
    private GameObject enemyObj;
    public GameObject dummyObj;

    private Rigidbody rb;

    private bool collidedAlly, alive=true;

    private Vector3 dummyPos;
    private Vector3 dummyDir;
    private Vector3 myPos;
    private Vector3 myDir;
	
	private Animator anim;
	
	public CollisionCopyMovement ccm;
	public bool taken=false;


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
        enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
		anim=GetComponentInChildren<Animator>();



        //dummyArr = GameObject.FindGameObjectsWithTag("Dummy");

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
            GetComponent<SphereCollider>().enabled = false;
            myPos = rb.position;
            float min = 99999;
            float temp;
            int dummyIndex = 0;
            dummyArr = other.gameObject.GetComponent<CollisionCopyMovement>().dummyArr;
            for (int i = 0; i < dummyArr.Length; i++)
            {
                
                if(dummyArr[i] != null)
                {
                    if (dummyArr[i].gameObject.GetComponent<CollisionCopyMovement>().isTaken())
                    {
                        continue;
                    }
                    //temp = Mathf.Abs(myPos.z) - Mathf.Abs(x.GetComponent<Rigidbody>().position.z);
                    temp = Vector3.Distance(myPos, dummyArr[i].GetComponent<Rigidbody>().position);
                    if (temp < min)
                    {
                        dummyIndex = i;
                        min = temp;
                    }
                       
                }
               
            }
            if(dummyArr[dummyIndex] != null)
            {
                dummyArr[dummyIndex].GetComponent<CollisionCopyMovement>().setTaken();
            }
            
            dummyObj = dummyArr[dummyIndex];
            //Invoke("setDieAnim", 1);
            collidedAlly = true;
            
            //ccm =other.gameObject.GetComponent<CollisionCopyMovement>();
            //if (!ccm.isTaken())
            //{

            //ccm.setTaken();
            //taken=true;

            
            //dummyObj = other.gameObject;
			//Debug.Log("Enemy collider disabled, " +  dummyObj);
			
			
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
                Debug.Log(dummyObj + "inside collided ally");
                dummyPos = dummyObj.GetComponent<Rigidbody>().position;
                myPos = rb.position;
                //myDir = dummyPos - myPos;
                myDir = Vector3.MoveTowards(myPos, dummyPos, 100);
                //dummyDir = myPos - dummyPos;
                dummyDir = Vector3.MoveTowards(dummyPos, myPos, 100);
                myDir.Normalize();
                myDir.z = myDir.z * -1;
                myDir.x = myDir.x * -1;
                dummyDir.Normalize();
                rb.velocity = myDir * 2;
                dummyObj.GetComponent<Rigidbody>().velocity = dummyDir * 2;
                Invoke("setDieAnim", 1);
            }

            if (rb.velocity.magnitude>1)   
			anim.SetBool("isRunning", true);
		else if(rb.velocity.magnitude<=1)   
			anim.SetBool("isRunning", false);
    }
	}
}