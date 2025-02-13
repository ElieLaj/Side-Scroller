using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.RegisterObstacles(this);
    }
    
}
