using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] private SnakeTail _nextTail;

    private Queue<Vector3> _positionHistory = new Queue<Vector3>();
    private Vector3 _initialPosition;

    void Awake()
    {
        _initialPosition = transform.position;
    }

    void Start()
    {
        ForceResetPosition();
    }

    public void ForceResetPosition()
    {
        transform.position = _initialPosition;
        _positionHistory.Clear();

        for (int i = 0; i < 5; i++)
        {
            _positionHistory.Enqueue(_initialPosition);
        }
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        _positionHistory.Enqueue(newPosition);

        if (_positionHistory.Count > 0)
        {
            transform.position = _positionHistory.Dequeue();
        }

        LookAtMovementDirection(newPosition);

        if (_nextTail != null)
        {
            _nextTail.UpdatePosition(transform.position);
        }
    }

    private void LookAtMovementDirection(Vector3 newPosition)
    {
        if (transform.position != newPosition)
        {
            Vector3 direction = (newPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
            }
        }
    }

    public SnakeTail GetNextTail()
    {
        return _nextTail;
    }

    public void ForceSetInitialPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        _positionHistory.Clear();

        for (int i = 0; i < 5; i++)
        {
            _positionHistory.Enqueue(newPosition);
        }
    }
}