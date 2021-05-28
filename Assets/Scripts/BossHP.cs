using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BossHP : MonoBehaviour
{
	//hpbar
	public const float maxHP= 100;
	public float currentHP = maxHP;
	public RectTransform hpbar;
	
	public PlayerController Pl2;
	
	//hpbar
	
	private Animator anim;
	
	//chase
	public Transform Player;
	int moveSpeed = 4;
	float maxDist = 3;
	float minDist = 1.5f;
	public static bool active=false, elem=true;
	private bool won;
	
	public static void setActive()
	{
		active=true;
	}
	public static void setInactive()
	{
		active=false;
	}
	
	public bool isActive()
	{
		return active;
	}
	
	public void takeDamage(float amount)
	{
		currentHP-=amount;
		if (currentHP<=0)
		{
			currentHP=0;
			//status-dead, play death anim
			setInactive();
			anim.SetBool("isFighting",false);
			anim.SetTrigger("dead");
			Pl2.setDance();
			//stop controller
			//setInactive();
			
			//switch to next level after 5 seconds.
			Invoke("setWin", 3.0f);
		}
		hpbar.sizeDelta = new Vector2(currentHP * 2, hpbar.sizeDelta.y);
	}
	
	void setWin(){

	SoundManager.playWinSound();
	Scene scene = SceneManager.GetActiveScene();
	won=false;
	elem=true;
	if (scene.name == "Level2")
	SceneManager.LoadScene("Level3");
	else if (scene.name == "Level3"){}
	else if (scene.name == "Level1")
	SceneManager.LoadScene("Level2"	);
	}
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponentInChildren<Animator>();
    }
	
	void initFightOnce()
    {
		if (elem)
		{
        anim.SetBool("isFighting",true);
		elem=false;
		}
    }
	

    // Update is called once per frame
    void Update()
    {
	//	Debug.Log(Vector3.Distance(transform.position, Player.position));
        transform.LookAt(Player);
		if (active){
		if (Vector3.Distance(transform.position, Player.position) >= minDist)
         {
             transform.position += transform.forward * moveSpeed * Time.deltaTime;

             if (Vector3.Distance(transform.position, Player.position) <= maxDist)
             {
				 initFightOnce();
				 //
             }
		 }
		}
    }
}
