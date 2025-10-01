using UnityEngine;

public class UniversalBoost : MonoBehaviour
{
    public enum BoostType { Speed, Magnet }

    [SerializeField] private BoostType _type;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private int _points = 3;

    public int Points => _points;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            PlayerBoostEffects boost = other.GetComponent<PlayerBoostEffects>();

            if (boost != null)
            {
                switch (_type)
                {
                    case BoostType.Speed:
                        boost.HandleSpeedBoostCollected(_points);
                        break;
                    case BoostType.Magnet:
                        boost.HandleMagnetBoostCollected(_points);
                        break;
                }
                gameObject.SetActive(false);
            }
        }
    }
}