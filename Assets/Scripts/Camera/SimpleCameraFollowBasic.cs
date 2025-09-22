using UnityEngine;

public class SimpleCameraFollowBasic : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 5f, -5f);

    private void LateUpdate()
    {
        if (_target != null)
        {
            transform.position = _target.position + _offset;
            transform.LookAt(_target);
        }
    }
}