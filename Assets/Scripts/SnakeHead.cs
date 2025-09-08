using System.Collections;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private float _moveStepSize = 0.1f;
    [SerializeField] private float _tailDistance = 1.0f;
    [SerializeField] private SnakeTail _firstTail;
    [SerializeField] private SnakeTail _tailPrefab;
    [SerializeField] private TailTip _tailTip;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private int _tailCount = 0;
    private Coroutine _moveCoroutine;
    private Coroutine _speedBoostCoroutine;
    private WaitForSeconds _waitForDelay;
    private bool _isGameActive = true;
    private float _currentDelay;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _currentDelay = _delay;
        _waitForDelay = new WaitForSeconds(_currentDelay);
    }

    private void Start()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        ForceSetTailPositions();
        MoveForward();
    }

    private void OnEnable()
    {
        Food.OnFoodEaten += HandleFoodEaten;
        Wall.OnGameOver += HandleGameOver;
        SpeedBoost.OnSpeedBoostCollected += HandleSpeedBoostCollected;
    }

    private void OnDisable()
    {
        Food.OnFoodEaten -= HandleFoodEaten;
        Wall.OnGameOver -= HandleGameOver;
        SpeedBoost.OnSpeedBoostCollected -= HandleSpeedBoostCollected;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }

        if (_speedBoostCoroutine != null)
        {
            StopCoroutine(_speedBoostCoroutine);
            _speedBoostCoroutine = null;
        }
    }

    private void HandleFoodEaten(Food food)
    {
        AddTail();
    }

    private void HandleSpeedBoostCollected(SpeedBoost boost)
    {
        if (_speedBoostCoroutine != null)
        {
            StopCoroutine(_speedBoostCoroutine);
        }
        _speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(boost.GetBoostMultiplier(), boost.GetBoostDuration()));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        float originalDelay = _delay;
        _currentDelay = _delay / multiplier;
        _waitForDelay = new WaitForSeconds(_currentDelay);

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveRoutine());
        }

        yield return new WaitForSeconds(duration);

        _currentDelay = originalDelay;
        _waitForDelay = new WaitForSeconds(_currentDelay);

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveRoutine());
        }

        _speedBoostCoroutine = null;
    }

    private void HandleGameOver()
    {
        _isGameActive = false;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }

        if (_speedBoostCoroutine != null)
        {
            StopCoroutine(_speedBoostCoroutine);
            _speedBoostCoroutine = null;
        }

        Debug.Log("Snake stopped moving. Game over!");
    }

    private void ForceSetTailPositions()
    {
        if (_firstTail != null)
        {
            Vector3 tail1Position = _initialPosition - transform.forward * _tailDistance;
            _firstTail.ForceSetInitialPosition(tail1Position);
            _tailCount = 1;
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (_isGameActive)
        {
            Vector3 previousPosition = transform.position;
            transform.position += transform.forward * _moveStepSize;

            if (_firstTail != null)
            {
                _firstTail.UpdatePosition(previousPosition);
            }

            if (_tailTip != null)
            {
                SnakeTail lastTail = GetLastTail();
                if (lastTail != null)
                {
                    _tailTip.UpdatePosition(lastTail.transform.position);
                }
            }

            yield return _waitForDelay;
        }
    }

    public void MoveForward()
    {
        if (_isGameActive == false)
            return;

        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MoveRoutine());
    }

    public void Rotate(Quaternion quaternion)
    {
        if (_isGameActive == false)
            return;

        transform.rotation = quaternion;
    }

    private void AddTail()
    {
        if (_tailPrefab == null || _isGameActive == false)
            return;

        SnakeTail lastTail = GetLastTail();
        if (lastTail == null)
            return;

        SnakeTail newTail = Instantiate(_tailPrefab, lastTail.transform.position, _tailPrefab.transform.rotation);
        newTail.name = "Tail_" + (_tailCount + 1);
        lastTail.SetNextTail(newTail);
        _tailCount++;

        if (_tailTip != null)
            _tailTip.OnTailAdded(newTail);
        else
            Debug.LogWarning("SnakeHead: TailTip is not assigned! The tip of the tail will not be updated.");
    }

    private SnakeTail GetLastTail()
    {
        if (_firstTail == null)
            return null;

        SnakeTail currentTail = _firstTail;

        while (currentTail.GetNextTail() != null)
            currentTail = currentTail.GetNextTail();

        return currentTail;
    }
}