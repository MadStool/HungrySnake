using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private SnakeHealth _snakeHealth;

    private void Awake()
    {
        if (_snakeHealth == null)
        {
            throw new System.Exception("HealthUI: SnakeHealth reference is not assigned! Please assign SnakeHealth component in the inspector.");
        }

        if (_livesText == null)
        {
            throw new System.Exception("HealthUI: LivesText reference is not assigned! Please assign TextMeshProUGUI component in the inspector.");
        }
    }

    private void Start()
    {
        _snakeHealth.OnLivesChanged += UpdateLivesDisplay;

        UpdateLivesDisplay(_snakeHealth.GetCurrentLives());
    }

    private void OnDestroy()
    {
        if (_snakeHealth != null)
            _snakeHealth.OnLivesChanged -= UpdateLivesDisplay;
    }

    private void UpdateLivesDisplay(int currentLives)
    {
        _livesText.text = $"X {currentLives}";
    }
}