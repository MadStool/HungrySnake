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
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private float _musicFadeDuration = 1f;

    [Header("References")]
    [SerializeField] private SnakeHead _snakeHead; // Добавляем ссылку на змею

    private AudioSource _audioSource;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        // Проверяем что все ссылки установлены
        if (_snakeHead == null)
        {
            throw new System.Exception("GameEndScreen: SnakeHead reference is not assigned! Please assign SnakeHead component in the inspector.");
        }

        if (_endGamePanel == null)
        {
            throw new System.Exception("GameEndScreen: EndGamePanel reference is not assigned!");
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _soundVolume;
        _audioSource.playOnAwake = false;

        _canvasGroup = _endGamePanel.GetComponent<CanvasGroup>();

        if (_canvasGroup == null)
        {
            Debug.LogError("CanvasGroup was not found on the panel! Add it manually in the editor.");
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
        // Подписываемся на нестатическое событие через ссылку на объект
        _snakeHead.OnGameOver += ShowDeathScreen;
    }

    private void OnDisable()
    {
        // Отписываемся от события
        if (_snakeHead != null)
        {
            _snakeHead.OnGameOver -= ShowDeathScreen;
        }
    }

    private void ShowDeathScreen()
    {
        StartCoroutine(ShowDeathScreenWithDelay());
    }

    private IEnumerator ShowDeathScreenWithDelay()
    {
        if (_backgroundMusic != null)
            StartCoroutine(FadeOutMusic());

        if (_deathSound != null)
            _audioSource.PlayOneShot(_deathSound);

        yield return new WaitForSecondsRealtime(_appearanceDelay);

        ShowEndScreen();
        StartCoroutine(FadeInScreen());
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = _backgroundMusic.volume;
        float elapsedTime = 0f;

        while (elapsedTime < _musicFadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            _backgroundMusic.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / _musicFadeDuration);
            yield return null;
        }

        _backgroundMusic.volume = 0f;
        _backgroundMusic.Stop();
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

        if (_backgroundMusic != null)
        {
            _backgroundMusic.volume = 1f;
            _backgroundMusic.Play();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}