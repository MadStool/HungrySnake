using UnityEngine;
using TMPro;

public class ScoreOnCamera : MonoBehaviour
{
    [Header("Screen Space UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _foodPoints = 1;
    [SerializeField] private int _speedBoostPoints = 5;
    [SerializeField] private int _magnetBoostPoints = 3;

    private int _currentScore = 0;

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

    private void HandleFoodEaten(Vector3 position) => AddPoints(_foodPoints);
    private void HandleSpeedBoostCollected(Vector3 position) => AddPoints(_speedBoostPoints);
    private void HandleMagnetBoostCollected(Vector3 position) => AddPoints(_magnetBoostPoints);

    public void AddPoints(int points)
    {
        _currentScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
            _scoreText.text = $"Score: {_currentScore}";
    }
}