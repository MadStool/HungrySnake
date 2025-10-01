using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GameEndScreen : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image _endGamePanel;
    [SerializeField] private Image _snakeImage;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Button _restartButton;

    [Header("Sprites")]
    [SerializeField] private Sprite _snakeHurtSprite;

    [Header("Appearance Settings")]
    [SerializeField] private float _appearanceDelay = 1f;
    [SerializeField] private float _fadeInDuration = 0.5f;

    [Header("Sound")]
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _soundVolume = 1f;

    private AudioSource _audioSource;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _soundVolume;
        _audioSource.playOnAwake = false;

        _canvasGroup = _endGamePanel.GetComponent<CanvasGroup>();

        if (_canvasGroup == null)
        {
            Debug.LogError("CanvasGroup не найден на панели! ƒобавь его вручную в редакторе.");
            return;
        }

        if (_endGamePanel != null)
        {
            _endGamePanel.gameObject.SetActive(false);
            _canvasGroup.alpha = 0f;
        }

        if (_restartButton != null)
            _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnEnable()
    {
        SnakeHead.OnGameOver += ShowDeathScreen;
    }

    private void OnDisable()
    {
        SnakeHead.OnGameOver -= ShowDeathScreen;
    }

    private void ShowDeathScreen()
    {
        StartCoroutine(ShowDeathScreenWithDelay());
    }

    private IEnumerator ShowDeathScreenWithDelay()
    {
        if (_deathSound != null)
        {
            _audioSource.PlayOneShot(_deathSound);
        }

        yield return new WaitForSecondsRealtime(_appearanceDelay);

        ShowEndScreen();
        StartCoroutine(FadeInScreen());
    }

    private void ShowEndScreen()
    {
        if (_endGamePanel != null)
            _endGamePanel.gameObject.SetActive(true);

        if (_snakeImage != null && _snakeHurtSprite != null)
            _snakeImage.sprite = _snakeHurtSprite;

        if (_resultText != null)
            _resultText.text = "Game Over!\nYou Died";

        Time.timeScale = 0f;
    }

    private IEnumerator FadeInScreen()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _fadeInDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            _canvasGroup.alpha = Mathf.Clamp01(elapsedTime / _fadeInDuration);
            yield return null;
        }

        _canvasGroup.alpha = 1f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}