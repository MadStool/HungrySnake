using System.Collections;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private float _moveStepSize = 0.1f;
    [SerializeField] private float _tailDistance = 1.0f;
    [SerializeField] private SnakeTail _firstTail;
    [SerializeField] private SnakeTail _tailPrefab;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private int _tailCount = 0;
    private Coroutine _moveCoroutine;
    private WaitForSeconds _waitForDelay;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _waitForDelay = new WaitForSeconds(_delay);
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
    }

    private void OnDisable()
    {
        Food.OnFoodEaten -= HandleFoodEaten;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    private void HandleFoodEaten(Food food)
    {
        AddTail();
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
        while (true)
        {
            Vector3 previousPosition = transform.position;
            transform.position += transform.forward * _moveStepSize;

            if (_firstTail != null)
            {
                _firstTail.UpdatePosition(previousPosition);
            }

            yield return _waitForDelay;
        }
    }

    public void MoveForward()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MoveRoutine());
    }

    public void Rotate(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    private void AddTail()
    {
        if (_tailPrefab == null)
            return;

        SnakeTail lastTail = GetLastTail();

        if (lastTail == null)
            return;

        SnakeTail newTail = Instantiate(_tailPrefab, lastTail.transform.position, _tailPrefab.transform.rotation);
        newTail.name = "Tail_" + (_tailCount + 1);
        lastTail.SetNextTail(newTail);
        _tailCount++;
    }

    private SnakeTail GetLastTail()
    {
        if (_firstTail == null)
            return null;

        SnakeTail currentTail = _firstTail;

        while (currentTail.GetNextTail() != null)
        {
            currentTail = currentTail.GetNextTail();
        }

        return currentTail;
    }
}