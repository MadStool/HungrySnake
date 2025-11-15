using UnityEngine;

public class PickupEffect : MonoBehaviour
{
    public enum ItemType
    {
        Food,
        Boost,
        Coin
    }

    [Header("Systems of particles")]
    public ParticleSystem foodParticles;
    public ParticleSystem boostParticles;
    public ParticleSystem coinParticles;

    public void PlayPickupEffect(Vector3 position, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Food:
                PlayParticles(foodParticles, position);
                break;
            case ItemType.Boost:
                PlayParticles(boostParticles, position);
                break;
            case ItemType.Coin:
                PlayParticles(coinParticles, position);
                break;
            default:
                Debug.LogWarning($"Unknown item type: {itemType}");
                break;
        }
    }

    private void PlayParticles(ParticleSystem particles, Vector3 position)
    {
        if (particles != null)
        {
            particles.transform.position = position;
            particles.Play();
        }
    }
}