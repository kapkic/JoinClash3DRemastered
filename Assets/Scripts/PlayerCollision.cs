using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public PlayerController movement;
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Saw" || collisionInfo.collider.tag == "Block" || collisionInfo.collider.tag == "Enemy")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
            
        }
    }
}
