using UnityEngine;
using TMPro;

public class TotalScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _screenScoreText;
    private int _totalScore = 0;

    private void Start()
    {
        Food.OnFoodEaten += HandleFoodEaten;
        PlayerBoostEffects.OnSpeedBoostCollected += HandleSpeedBoostCollected;
        PlayerBoostEffects.OnMagnetBoostCollected += HandleMagnetBoostCollected;

        UpdateScoreText();
    }

    private void OnDestroy()
    {
        Food.OnFoodEaten -= HandleFoodEaten;
        PlayerBoostEffects.OnSpeedBoostCollected -= HandleSpeedBoostCollected;
        PlayerBoostEffects.OnMagnetBoostCollected -= HandleMagnetBoostCollected;
    }

    private void HandleFoodEaten(Vector3 position, int points, Color color) => AddPoints(points);
    private void HandleSpeedBoostCollected(Vector3 position, int points) => AddPoints(points);
    private void HandleMagnetBoostCollected(Vector3 position, int points) => AddPoints(points);

    public void AddPoints(int points)
    {
        _totalScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_screenScoreText != null)
            _screenScoreText.text = $"Score: {_totalScore}";
    }

    public int GetTotalScore() => _totalScore;
}