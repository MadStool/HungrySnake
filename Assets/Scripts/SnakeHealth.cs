using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private int _currentLives = 3;

    [Header("Knockback Settings")]
    [SerializeField] private float _knockbackDistance = 2f;
    [SerializeField] private float _knockbackSpeed = 8f;
    [SerializeField] private float _slowDuration = 2f;
    [SerializeField] private float _slowSpeedMultiplier = 0.4f;

    [Header("References")]
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private LayerMask _obstacleLayer;

    private bool _isInvincible = false;
    private Coroutine _knockbackCoroutine;

    public event System.Action<int> OnLivesChanged;
    public event System.Action OnHit;

    private void Start()
    {
        _currentLives = _maxLives;
        OnLivesChanged?.Invoke(_currentLives);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _obstacleLayer) != 0)
            if (_isInvincible == false)
                HandleCollision();
    }

    private void HandleCollision()
    {
        _currentLives--;
        OnLivesChanged?.Invoke(_currentLives);
        OnHit?.Invoke();

        Debug.Log($"Lives left: {_currentLives}");

        if (_currentLives <= 0)
        {
            Debug.Log("No lives left - Game Over!");
            _snakeHead.HandleGameOver();
        }
        else
        {
            Debug.Log("Lives remaining - knockback");

            if (_knockbackCoroutine != null)
                StopCoroutine(_knockbackCoroutine);

            _knockbackCoroutine = StartCoroutine(KnockbackRoutine());
        }
    }

    private IEnumerator KnockbackRoutine()
    {
        _isInvincible = true;

        _snakeHead.SetSpeed(0f);

        yield return StartCoroutine(MoveSnakeBackwards());

        float baseSpeed = _snakeHead.GetBaseSpeed();
        float slowSpeed = baseSpeed * _slowSpeedMultiplier;
        _snakeHead.SetSpeed(slowSpeed);

        yield return new WaitForSeconds(_slowDuration);

        _snakeHead.SetSpeed(baseSpeed);
        _isInvincible = false;
    }

    private IEnumerator MoveSnakeBackwards()
    {
        float distanceMoved = 0f;

        _snakeHead.SetSpeed(0f);

        List<SnakeTail> allTailSegments = _snakeHead.GetAllTailSegments();
        TailTip tailTip = _snakeHead.GetTailTip();

        Debug.Log($"Moving {allTailSegments.Count} tail segments backwards");

        if (tailTip != null)
            tailTip.enabled = false;

        while (distanceMoved < _knockbackDistance)
        {
            float step = _knockbackSpeed * Time.deltaTime;
            Vector3 moveVector = -_snakeHead.transform.forward * step;

            _snakeHead.transform.position += moveVector;

            foreach (SnakeTail tail in allTailSegments)
                if (tail != null)
                    tail.transform.position += moveVector;

            if (tailTip != null)
                tailTip.transform.position += moveVector;

            distanceMoved += step;
            yield return null;
        }

        if (tailTip != null)
        {
            tailTip.enabled = true;
            tailTip.ForceResetPosition();
        }

        SyncTailPositions(allTailSegments);
    }

    private void SyncTailPositions(List<SnakeTail> tailSegments)
    {
        if (tailSegments.Count == 0)
            return;

        SnakeTail firstTail = tailSegments[0];

        if (firstTail != null)
        {
            Vector3 expectedTailPosition = _snakeHead.transform.position - _snakeHead.transform.forward * _snakeHead.GetTailDistance();
            firstTail.ForceSetInitialPosition(expectedTailPosition);
        }

        Debug.Log($"Synchronized {tailSegments.Count} tail segments after knockback");
    }

    public void AddLife()
    {
        if (_currentLives < _maxLives)
        {
            _currentLives++;
            OnLivesChanged?.Invoke(_currentLives);
        }
    }

    public int GetCurrentLives() => _currentLives;
    public bool IsInvincible() => _isInvincible;
}