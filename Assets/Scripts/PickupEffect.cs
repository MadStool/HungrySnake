using UnityEngine;

public class PickupEffect : MonoBehaviour
{
    public const string FOOD = "Food";
    public const string BOOST = "Boost";
    public const string COIN = "Coin";

    [Header("Системы частиц для разных типов")]
    public ParticleSystem foodParticles;
    public ParticleSystem boostParticles;
    public ParticleSystem coinParticles;

    public void PlayPickupEffect(Vector3 position, string itemType)
    {
        switch (itemType)
        {
            case FOOD:
                PlayParticles(foodParticles, position);
                break;
            case BOOST:
                PlayParticles(boostParticles, position);
                break;
            case COIN:
                PlayParticles(coinParticles, position);
                break;
            default:
                Debug.LogWarning($"Неизвестный тип предмета: {itemType}");
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