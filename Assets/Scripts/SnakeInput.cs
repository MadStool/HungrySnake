using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private KeyCode _downKey = KeyCode.S;
    [SerializeField] private KeyCode _upKey = KeyCode.W;

    private Vector3 _lastDirection = Vector3.forward;

    private void Update()
    {
        if (Input.GetKeyDown(_upKey) && _lastDirection != Vector3.back)
        {
            _snakeHead.Rotate(Quaternion.Euler(0, 0, 0));
            _lastDirection = Vector3.forward;
        }
        else if (Input.GetKeyDown(_downKey) && _lastDirection != Vector3.forward)
        {
            _snakeHead.Rotate(Quaternion.Euler(0, 180, 0));
            _lastDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(_rightKey) && _lastDirection != Vector3.left)
        {
            _snakeHead.Rotate(Quaternion.Euler(0, 90, 0));
            _lastDirection = Vector3.right;
        }
        else if (Input.GetKeyDown(_leftKey) && _lastDirection != Vector3.right)
        {
            _snakeHead.Rotate(Quaternion.Euler(0, -90, 0));
            _lastDirection = Vector3.left;
        }
    }
}