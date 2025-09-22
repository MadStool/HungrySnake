using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private float _keyboardSteerSpeed = 90f;

    private void Update()
    {
        if (_snakeHead == null) return;

        float rotation = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            rotation = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            rotation = 1f;

        if (rotation != 0f)
        {
            _snakeHead.Rotate(_snakeHead.transform.rotation * Quaternion.Euler(0, rotation * _keyboardSteerSpeed * Time.deltaTime, 0));
        }
    }
}