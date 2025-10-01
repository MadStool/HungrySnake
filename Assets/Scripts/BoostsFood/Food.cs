using UnityEngine;

public class Food : MonoBehaviour
{
    public static event System.Action<Vector3, int, Color> OnFoodEaten;

    [Header("Food Settings")]
    [SerializeField] private int _points = 1;
    [SerializeField] private Color _textColor = Color.white;

    public int Points => _points;
    public Color TextColor => _textColor;

    public void OnEaten()
    {
        OnFoodEaten?.Invoke(transform.position, _points, _textColor);
        gameObject.SetActive(false);
    }
}