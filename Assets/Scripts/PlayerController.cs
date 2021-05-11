using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed = 0;
	//private bool join = false; 
	private int count;
	private bool boost, won, jump,fighting;
	private bool runbefore=true;
	int timer;
	public TextMeshProUGUI countText;
	private Animator anim;

	public BossHP hp2;

	private Rigidbody rb;
	private float movementX, movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;

		SetCountText();
		anim=GetComponentInChildren<Animator>();
		//winTextObject.SetActive(false);
		//lossTextObject.SetActive(false);
	}
		
	void OnMove(InputValue movementValue)
	{
		//body
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
		if (movementY<0)
		movementY=0;
		
		
	}

	void SetCountText()
    {
		countText.text = count.ToString();
		if (count > 4)
        {
			//winTextObject.SetActive(true);
			
		}
    }
	
	void setWin(){

	SoundManager.playWinSound();
	Scene scene = SceneManager.GetActiveScene();
	won=false;
	if (scene.name == "Level2")
	SceneManager.LoadScene("Level3");
	else if (scene.name == "Level3"){}
	else if (scene.name == "Level1")
	SceneManager.LoadScene("Level2"	);
	}

	void SetSpeedText()
	{
		//speedText.text = "Speed: " + String.Format("{0:0.0}", rb.velocity.magnitude);
	}
	
	void setJoin()
	{
		SoundManager.playJoinSound();
	}
	
	void setJump()
	{
		SoundManager.playJumpSound();
	}

	void setGameOver()
	{
		//lossTextObject.SetActive(true);
		SoundManager.playLossSound();
	}
	
	void setDie()
	{
		//lossTextObject.SetActive(true);
		SoundManager.playPopSound();
		BossHP.setInactive();
		//if all dies
		//setGameOver();
	}

	//physics go to fixedupdate
	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);
		
		if(rb.velocity.magnitude>1)   
			anim.SetBool("isRunning", true);
		else if(rb.velocity.magnitude<=1)   
			anim.SetBool("isRunning", false);
		
		if (jump)
		{
			anim.SetTrigger("jumping");
/*			timer++;
			
            if (timer<5)
            {*/
                Vector3 movement2 = new Vector3(0.0f, 600.0f, 50.0f);
				rb.AddForce(movement2);
            /*}
			timer=0;*/
			jump=false;
		}
		
		if (boost)
		{
			timer++;
            if (timer<150)
            {
                speed+= 10;
            }
			timer=0;
			boost=false;
		}
		if (rb.position.y < -1f)
        {
           FindObjectOfType<GameManager>().EndGame();
        }
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Coin"))
		{
			other.gameObject.SetActive(false);
			count++;
			SetCountText();
			SoundManager.playCoinSound();
		}
		else if (other.gameObject.CompareTag("Speed"))
		{
			other.gameObject.SetActive(false);
			boost=true;
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
		if (other.gameObject.CompareTag("Ramp"))
        {
			setJump();
			jump=true;
        }
		if (other.gameObject.CompareTag("BossStep"))
        {
		//setFight
		//other.gameObject.SetActive(false);
		BossHP.setActive();
		setFight();			
        }
		if (other.gameObject.CompareTag("WinStep"))
		{
		//won=true;
		Invoke("setWin", 5.0f);	
		}
	}
	void setFight()
	{
		fighting=true;
		
	}


    // Update is called once per frame
    void Update()
    {

		transform.rotation = Input.gyro.attitude;
		
		//can make it a separate script.
	if (fighting)
	{
		hp2.takeDamage(1);
		//fighting=false;
	}
	
	/*if (won && runbefore){
	runbefore=false;
	SoundManager.playWinSound();
	Scene scene = SceneManager.GetActiveScene();
	won=false;
	if (scene.name == "Level2")
	SceneManager.LoadScene("Level3");
	else if (scene.name == "Level3"){}
	else if (scene.name == "Level1")
	SceneManager.LoadScene("Level2"	);
	}*/
	//SceneManager.LoadScene("WinLevel", LoadSceneMode.Additive);
	
	}
}
