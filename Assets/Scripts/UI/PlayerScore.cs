using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [Header("Score Values")]
    [SerializeField] private int _foodPoints = 1;
    [SerializeField] private int _speedBoostPoints = 5;
    [SerializeField] private int _magnetBoostPoints = 3;

    [Header("References")]
    [SerializeField] private ScoreOverHead _scoreOverHead;
    [SerializeField] private ScoreOnCamera _scoreOnCamera;

    private int _currentScore = 0;

    private void Start()
    {
        Food.OnFoodEaten += HandleFoodEaten;
        PlayerBoostEffects.OnSpeedBoostCollected += HandleSpeedBoostCollected;
        PlayerBoostEffects.OnMagnetBoostCollected += HandleMagnetBoostCollected;
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

        if (_scoreOverHead != null)
            _scoreOverHead.UpdateScoreText(_currentScore);

        if (_scoreOnCamera != null)
            _scoreOnCamera.AddPoints(points);
    }
}