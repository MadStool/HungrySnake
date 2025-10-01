using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _screenScoreText;
    [SerializeField] private ScoreOverHead _scoreOverHead;

    private int _currentScore = 0;

    private void Start()
    {
        Food.OnFoodEaten += HandleFoodEaten;
        PlayerBoostEffects.OnSpeedBoostCollected += HandleSpeedBoostCollected;
        PlayerBoostEffects.OnMagnetBoostCollected += HandleMagnetBoostCollected;

        UpdateAllScoreTexts();
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
        _currentScore += points;
        UpdateAllScoreTexts();
    }

    private void UpdateAllScoreTexts()
    {
        if (_screenScoreText != null)
            _screenScoreText.text = $"Score: {_currentScore}";

        if (_scoreOverHead != null)
            _scoreOverHead.UpdateScoreText(_currentScore);
    }
}