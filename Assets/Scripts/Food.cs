using UnityEngine;

public class Food : MonoBehaviour
{
    public static event System.Action<Food> OnFoodEaten;

    [SerializeField] private Vector2 _spawnAreaMin = new Vector2(-10, -10);
    [SerializeField] private Vector2 _spawnAreaMax = new Vector2(10, 10);
    [SerializeField] private float _spawnHeight = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SnakeHead>() != null)
        {
            OnFoodEaten?.Invoke(this);
            Respawn();
        }
    }

    public void Respawn()
    {
        float randomX = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
        float randomZ = Random.Range(_spawnAreaMin.y, _spawnAreaMax.y);
        transform.position = new Vector3(randomX, _spawnHeight, randomZ);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((_spawnAreaMin.x + _spawnAreaMax.x) / 2, _spawnHeight, (_spawnAreaMin.y + _spawnAreaMax.y) / 2);
        Vector3 size = new Vector3(_spawnAreaMax.x - _spawnAreaMin.x, 0.1f, _spawnAreaMax.y - _spawnAreaMin.y);
        Gizmos.DrawWireCube(center, size);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}