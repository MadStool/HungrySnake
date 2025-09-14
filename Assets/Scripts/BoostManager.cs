using System.Collections;
using UnityEngine;

public class BoostManager : MonoBehaviour
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

    // Speed Boost methods
    public void HandleSpeedBoostCollected()
    {
        _activeSpeedBoosts++;

        if (_speedBoostCoroutine != null)
        {
            StopCoroutine(_speedBoostCoroutine);
        }

        _snakeHead.SetDelay(_originalBaseDelay / _speedBoostMultiplier);
        _speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(_speedBoostDuration);

        _activeSpeedBoosts--;

        if (_activeSpeedBoosts <= 0)
        {
            _snakeHead.SetDelay(_originalBaseDelay);
            _activeSpeedBoosts = 0;
        }

        _speedBoostCoroutine = null;
    }

    // Magnet Boost methods
    public void HandleMagnetBoostCollected()
    {
        _activeMagnetBoosts++;

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