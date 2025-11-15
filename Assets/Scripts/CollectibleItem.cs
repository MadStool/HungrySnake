using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Тип объекта")]
    public string itemType = PickupEffect.COIN;

    [Header("Слой игрока")]
    public LayerMask playerLayer;

    void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            PickupEffect effects = other.GetComponent<PickupEffect>();

            if (effects != null)
            {
                effects.PlayPickupEffect(transform.position, itemType);
            }

            Destroy(gameObject);
        }
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}