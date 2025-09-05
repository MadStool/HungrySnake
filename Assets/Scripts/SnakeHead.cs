using System.Collections;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private float _moveStepSize = 0.1f;
    [SerializeField] private float _tailDistance = 1.0f;
    [SerializeField] private SnakeTail _firstTail;

    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Start()
    {
        transform.position = _initialPosition;
        ForceSetTailPositions();
        MoveForward();
    }

    private void ForceSetTailPositions()
    {
        if (_firstTail != null)
        {
            Vector3 tail1Position = _initialPosition - transform.forward * _tailDistance;
            _firstTail.ForceSetInitialPosition(tail1Position);
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            var previousPosition = transform.position;
            transform.position += transform.forward * _moveStepSize;

            if (_firstTail != null)
            {
                _firstTail.UpdatePosition(previousPosition);
            }

            yield return new WaitForSeconds(_delay);
        }
    }

    public void MoveForward()
    {
        StartCoroutine(MoveRoutine());
    }

    public void Rotate(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }
}