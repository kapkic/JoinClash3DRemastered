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
	private bool join = false; 
	private int count;
	private bool boost, won;
	private bool runbefore=true;
	int timer;
	//public TextMeshProUGUI countText, speedText;
	//public GameObject winTextObject, lossTextObject;

	private Rigidbody rb;
	private float movementX, movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;

		SetCountText();
		//winTextObject.SetActive(false);
		//lossTextObject.SetActive(false);
	}
		
	void OnMove(InputValue movementValue)
	{
		//body
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
		
		
	}

	void SetCountText()
    {
		//countText.text = "Count: " + count.ToString();
		if (count > 4)
        {
			//winTextObject.SetActive(true);
			
		}
    }
	
	void setWin(){

	
	
	}

	void SetSpeedText()
	{
		//speedText.text = "Speed: " + String.Format("{0:0.0}", rb.velocity.magnitude);
	}
	
	void setJoin()
	{
		SoundManager.playJoinSound();
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
		//if all dies
		//setGameOver();
	}

	//physics go to fixedupdate
	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);
		
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
		if (other.gameObject.CompareTag("BossStep"))
        {
			//setFight
			other.gameObject.SetActive(false);
			won=true;
			setWin();		
        }
	}


    // Update is called once per frame
    void Update()
    {

		transform.rotation = Input.gyro.attitude;
		
		//can make it a separate script.
	
	if (won && runbefore){
	runbefore=false;
	SoundManager.playWinSound();
	Scene scene = SceneManager.GetActiveScene();
	won=false;
	if (scene.name == "Level2")
	SceneManager.LoadScene("Level3", LoadSceneMode.Additive);
	else if (scene.name == "Level3"){}
	else if (scene.name == "Level1")
	SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
	
	}
	//SceneManager.LoadScene("WinLevel", LoadSceneMode.Additive);
	
	}
}
