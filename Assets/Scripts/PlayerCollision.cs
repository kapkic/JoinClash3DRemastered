using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public KeyInput movement;
    //public GameManager gameManager;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
