using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
	public float speed = 0;
	private bool join = false; 
	private int count;
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
	SoundManager.playWinSound();
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
		SetSpeedText();
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
			speed += 10;
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
			setWin();		
        }
	}


    // Update is called once per frame
    void Update()
    {

		transform.rotation = Input.gyro.attitude;
	}
}
