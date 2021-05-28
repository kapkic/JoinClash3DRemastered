using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static AudioSource audioSource;
    public static AudioClip winClip, lossClip, beginClip, coinClip, speedClip, popClip, joinAClip, joinBClip, jumpClip;
    public float volume = 0.5f;
	public static int joinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        winClip = Resources.Load<AudioClip>("wintrim");
        lossClip = Resources.Load<AudioClip>("losstrim");
        //beginClip = Resources.Load<AudioClip>("open");
        speedClip = Resources.Load<AudioClip>("speedtrim");
        coinClip = Resources.Load<AudioClip>("cointrim");
        joinAClip = Resources.Load<AudioClip>("join-a");
        joinBClip = Resources.Load<AudioClip>("join-b");
		popClip = Resources.Load<AudioClip>("pop");
		jumpClip = Resources.Load<AudioClip>("jump");
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(winClip);
    }

    public static void playWinSound()
    {
   //     audioSource.PlayOneShot(winClip);
    }
	public static void playJumpSound()
    {
     //   audioSource.PlayOneShot(jumpClip);
    }
	public static void playPopSound()
    {
       // audioSource.PlayOneShot(popClip);
    }

    public static void playLossSound()
    {
        //audioSource.PlayOneShot(lossClip);
    }
    public static void playBeginSound()
    {
       // audioSource.PlayOneShot(beginClip);
    }
    public static void playCoinSound()
    {
        //audioSource.PlayOneShot(coinClip);
    }
    public static void playSpeedSound()
    {
        //audioSource.PlayOneShot(speedClip);
    }

    public static void playJoinSound()
    {
	/*	if (joinCount%2==0)
			audioSource.PlayOneShot(joinAClip);
		else
			audioSource.PlayOneShot(joinBClip);
		joinCount++;*/
    }
    public static void playJoinBSound()
    {
     //   audioSource.PlayOneShot(joinBClip);
    }

}
