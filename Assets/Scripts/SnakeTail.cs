using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] private SnakeTail _nextTail;
    [SerializeField] private int _historySize = 5;

    private Queue<Vector3> _positionHistory;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _positionHistory = new Queue<Vector3>(_historySize + 1);
    }

    void Start()
    {
        ForceResetPosition();
    }

    public void ForceResetPosition()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _positionHistory.Clear();

        for (int i = 0; i < _historySize; i++)
            _positionHistory.Enqueue(_initialPosition);
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        _positionHistory.Enqueue(newPosition);

        transform.position = _positionHistory.Dequeue();

        if (transform.position != newPosition)
        {
            Vector3 direction = (newPosition - transform.position);

            if (direction.sqrMagnitude > 0.001f)
            {
                direction.Normalize();
                transform.rotation = Quaternion.LookRotation(direction) * _initialRotation;
            }
        }

        if (_nextTail != null)
        {
            _nextTail.UpdatePosition(transform.position);
        }
    }

    public void ForceSetInitialPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        transform.rotation = _initialRotation;
        _positionHistory.Clear();

        for (int i = 0; i < _historySize; i++)
            _positionHistory.Enqueue(newPosition);
    }

    public SnakeTail GetNextTail() => _nextTail;
    public void SetNextTail(SnakeTail nextTail) => _nextTail = nextTail;
}