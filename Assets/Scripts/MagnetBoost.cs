using UnityEngine;

public class MagnetBoost : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            BoostManager boostManager = other.GetComponent<BoostManager>();
            if (boostManager != null)
            {
                boostManager.HandleMagnetBoostCollected();
                gameObject.SetActive(false);
            }
        }
    }
}