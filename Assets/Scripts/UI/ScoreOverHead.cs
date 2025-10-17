using UnityEngine;
using TMPro;

public class ScoreOverHead : MonoBehaviour
{
    [Header("World Space UI")]
    [SerializeField] private Canvas _scoreUIPrefab;
    [SerializeField] private Vector3 _worldOffset = new Vector3(0f, 2f, 0f);

    private Canvas _scoreUIInstance;
    private TextMeshProUGUI _scoreText;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        CreateWorldUI();
    }

    private void LateUpdate()
    {
        if (_scoreUIInstance != null && _mainCamera != null)
        {
            _scoreUIInstance.transform.position = transform.position + _worldOffset;
            _scoreUIInstance.transform.LookAt(_mainCamera.transform);
            _scoreUIInstance.transform.Rotate(0, 180, 0);
        }
    }

    private void CreateWorldUI()
    {
        if (_scoreUIPrefab != null)
        {
            _scoreUIInstance = Instantiate<Canvas>(_scoreUIPrefab, transform.position + _worldOffset, Quaternion.identity);
            _scoreText = _scoreUIInstance.GetComponentInChildren<TextMeshProUGUI>();

            _scoreUIInstance.transform.SetParent(transform);
        }
    }

    public void UpdateScoreText(string text)
    {
        if (_scoreText != null)
            _scoreText.text = text;
    }
}