using UnityEngine;
using TMPro;
using System.Collections;

public class SimpleFloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textPrefab;
    [SerializeField] private float _floatHeight = 1f;
    [SerializeField] private float _floatDuration = 1f;
    [SerializeField] private Vector3 _offset = new Vector3(0.5f, 0f, 0f);

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        Food food = other.GetComponent<Food>();
        if (food != null)
        {
            ShowText($"+{food.Points}", food.TextColor);
        }
        else if (other.GetComponent<UniversalBoost>() != null)
        {
            ShowText("+Boost", Color.yellow);
        }
    }

    private void ShowText(string message, Color color)
    {
        if (_textPrefab == null)
            return;

        Vector3 spawnPosition = transform.position + _offset;
        TextMeshPro text = Instantiate(_textPrefab, spawnPosition, Quaternion.identity);
        text.text = message;
        text.color = color;
        text.transform.SetParent(transform);

        StartCoroutine(TextLookAtCamera(text));
        StartCoroutine(FloatAndFade(text, color));
    }

    private System.Collections.IEnumerator TextLookAtCamera(TextMeshPro text)
    {
        while (text != null)
        {
            if (_mainCamera != null)
            {
                text.transform.LookAt(_mainCamera.transform);
                text.transform.Rotate(0, 180, 0);
            }
            yield return null;
        }
    }

    private System.Collections.IEnumerator FloatAndFade(TextMeshPro text, Color startColor)
    {
        float elapsed = 0f;
        Vector3 startPosition = text.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * _floatHeight;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsed < _floatDuration && text != null)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / _floatDuration;

            text.transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            text.color = Color.Lerp(startColor, endColor, progress);

            yield return null;
        }

        if (text != null)
            Destroy(text.gameObject);
    }
}