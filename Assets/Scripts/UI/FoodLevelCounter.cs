using UnityEngine;

public class FoodLevelCounter : MonoBehaviour
{
    [SerializeField] private ScoreOverHead _scoreOverHead;
    private int _foodCount = 0;

    private void Start()
    {
        Food.OnFoodEaten += HandleFoodEaten;
        UpdateFoodText();
    }

    private void OnDestroy()
    {
        Food.OnFoodEaten -= HandleFoodEaten;
    }

    private void HandleFoodEaten(Vector3 position, int points, Color color)
    {
        _foodCount++;
        UpdateFoodText();
    }

    private void UpdateFoodText()
    {
        if (_scoreOverHead != null)
            _scoreOverHead.UpdateScoreText($"Level: {_foodCount}");
    }

    public int GetFoodCount() => _foodCount;
}