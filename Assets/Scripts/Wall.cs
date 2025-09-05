using UnityEngine;

public class Wall : MonoBehaviour
{
    public static event System.Action OnGameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SnakeHead>() != null)
        {
            Debug.Log("Game Over! Snake hit the wall.");
            OnGameOver?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SnakeHead>() != null)
        {
            Debug.Log("Game Over! Snake hit the wall.");
            OnGameOver?.Invoke();
        }
    }
}