using UnityEngine;

public class Food : MonoBehaviour
{
    public static event System.Action<Food> OnFoodEaten;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SnakeHead>() != null)
        {
            OnFoodEaten?.Invoke(this);

            gameObject.SetActive(false);
        }
    }
}