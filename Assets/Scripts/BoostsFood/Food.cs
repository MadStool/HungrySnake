using UnityEngine;

public class Food : MonoBehaviour
{
    public static event System.Action<Vector3> OnFoodEaten;

    public void OnEaten()
    {
        OnFoodEaten?.Invoke(transform.position);
        gameObject.SetActive(false);
    }
}