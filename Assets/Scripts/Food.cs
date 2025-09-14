using UnityEngine;

public class Food : MonoBehaviour
{
    public void OnEaten()
    {
        gameObject.SetActive(false);
    }
}