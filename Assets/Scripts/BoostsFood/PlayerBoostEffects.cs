using System.Collections;
using UnityEngine;

public class PlayerBoostEffects : MonoBehaviour
{
    [Header("Speed Boost Settings")]
    [SerializeField] private float _speedBoostMultiplier = 2f;
    [SerializeField] private float _speedBoostDuration = 3f;

    [Header("Magnet Boost Settings")]
    [SerializeField] private float _magnetDuration = 5f;
    [SerializeField] private float _magnetRadius = 3f;
    [SerializeField] private float _attractionSpeed = 2f;
    [SerializeField] private LayerMask _foodLayer;

    private SnakeHead _snakeHead;
    private Coroutine _speedBoostCoroutine;
    private Coroutine _magnetCoroutine;
    private float _originalBaseDelay;
    private int _activeSpeedBoosts = 0;
    private int _activeMagnetBoosts = 0;
    private bool _isMagnetActive = false;

    public static event System.Action<Vector3, int> OnSpeedBoostCollected;
    public static event System.Action<Vector3, int> OnMagnetBoostCollected;

    private void Awake()
    {
        _snakeHead = GetComponent<SnakeHead>();
        _originalBaseDelay = _snakeHead.GetOriginalDelay();
    }

    private void Update()
    {
        if (_isMagnetActive)
        {
            AttractFood();
        }
    }

    // Speed Boost
    public void HandleSpeedBoostCollected(int points)
    {
        OnSpeedBoostCollected?.Invoke(transform.position, points);

        _activeSpeedBoosts = 1;

        if (_speedBoostCoroutine != null)
            StopCoroutine(_speedBoostCoroutine);

        _snakeHead.SetSpeed(_snakeHead.GetBaseSpeed() * _speedBoostMultiplier);
        _speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(_speedBoostDuration);

        _activeSpeedBoosts--;

        if (_activeSpeedBoosts <= 0)
        {
            _snakeHead.SetSpeed(_snakeHead.GetBaseSpeed());
            _activeSpeedBoosts = 0;
        }

        _speedBoostCoroutine = null;
    }

    // Magnet Boost
    public void HandleMagnetBoostCollected(int points)
    {
        OnMagnetBoostCollected?.Invoke(transform.position, points);

        _activeMagnetBoosts = 1;

        if (_magnetCoroutine != null)
            StopCoroutine(_magnetCoroutine);

        _isMagnetActive = true;
        _magnetCoroutine = StartCoroutine(MagnetRoutine());
    }

    private IEnumerator MagnetRoutine()
    {
        yield return new WaitForSeconds(_magnetDuration);

        _activeMagnetBoosts--;

        if (_activeMagnetBoosts <= 0)
        {
            _isMagnetActive = false;
            _activeMagnetBoosts = 0;
        }

        _magnetCoroutine = null;
    }

    private void AttractFood()
    {
        Collider[] foodItems = Physics.OverlapSphere(_snakeHead.transform.position, _magnetRadius, _foodLayer);

        foreach (Collider foodCollider in foodItems)
        {
            if (foodCollider.gameObject.activeInHierarchy == false)
                continue;

            foodCollider.transform.position = Vector3.MoveTowards(
                foodCollider.transform.position,
                _snakeHead.transform.position,
                _attractionSpeed * Time.deltaTime
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_isMagnetActive && _snakeHead != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_snakeHead.transform.position, _magnetRadius);
        }
    }
}