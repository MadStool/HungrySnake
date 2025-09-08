using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour
{
    public static event System.Action<SpeedBoost> OnSpeedBoostCollected;

    [SerializeField] private float _boostMultiplier = 5f;
    [SerializeField] private float _boostDuration = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SnakeHead>() != null)
        {
            OnSpeedBoostCollected?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public float GetBoostMultiplier() => _boostMultiplier;
    public float GetBoostDuration() => _boostDuration;
}