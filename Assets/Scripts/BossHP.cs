using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BossHP : MonoBehaviour
{
	//hpbar
	public const int maxHP= 100;
	public int currentHP = maxHP;
	public RectTransform hpbar;
	//hpbar
	
	//private Animator anim;
	
	//chase
	public Transform Player;
	int moveSpeed = 4;
	int maxDist = 3;
	int minDist = 1;
	public static bool active=false;
	private bool won;
	
	public static void setActive()
	{
		active=true;
	}
	public static void setInactive()
	{
		active=false;
	}
	
	public void takeDamage(int amount)
	{
		currentHP-=amount;
		if (currentHP<=0)
		{
			currentHP=0;
			//status-dead, play death anim
			//anim.SetTrigger("dead");
			//stop controller
			setInactive();
			//switch to next level after 5 seconds.
			Invoke("setWin", 3.0f);
		}
		hpbar.sizeDelta = new Vector2(currentHP * 2, hpbar.sizeDelta.y);
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
    // Start is called before the first frame update
    void Start()
    {
        //anim=GetComponentInChildren<Animator>();
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
//
             }
		 }
		}
    }
}
