using UnityEngine;
using System.Collections.Generic;

public class TailTip : MonoBehaviour
{
    [SerializeField] private SnakeTail _targetTail;
    [SerializeField] private int _historySize = 5;

    private Queue<Vector3> _positionHistory;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _positionHistory = new Queue<Vector3>(_historySize + 1);

        if (_targetTail == null)
            throw new System.Exception("TailTip: The Target Tail is not assigned! Assign the last segment of the tail to the Inspector.");
    }

    private void Start()
    {
        ForceResetPosition();
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        _positionHistory.Enqueue(newPosition);

        if (_positionHistory.Count > _historySize)
        {
            transform.position = _positionHistory.Dequeue();
        }

        if (_positionHistory.Count > 1)
        {
            Vector3[] positions = _positionHistory.ToArray();
            Vector3 direction = (positions[positions.Length - 1] - positions[0]).normalized;

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction) * _initialRotation;
            }
        }
    }

    public void SetTargetTail(SnakeTail newTarget)
    {
        if (newTarget == null)
            throw new System.ArgumentNullException("NewTarget", "TailTip: You cannot set null as the target tail!");

        _targetTail = newTarget;
    }

    public void ForceResetPosition()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _positionHistory.Clear();

        for (int i = 0; i < _historySize; i++)
            _positionHistory.Enqueue(_initialPosition);
    }

    public void OnTailAdded(SnakeTail newLastTail)
    {
        if (newLastTail == null)
            throw new System.ArgumentNullException("newLastTail", "TailTip: You can't add a null tail!");

        SetTargetTail(newLastTail);
    }
}