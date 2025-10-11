using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
                GetComponent<AudioSource>().Play();

                switch (_type)
                {
                    case BoostType.Speed:
                        boost.HandleSpeedBoostCollected(_points);
                        break;
                    case BoostType.Magnet:
                        boost.HandleMagnetBoostCollected(_points);
                        break;
                }

                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                Destroy(gameObject, 2f);
            }
        }
    }
}