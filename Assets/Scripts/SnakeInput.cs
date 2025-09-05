using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private KeyCode _downKey = KeyCode.S;
    [SerializeField] private KeyCode _upKey = KeyCode.W;

    private void Update()
    {
        if (Input.GetKeyDown(_upKey))
            _snakeHead.Rotate(Quaternion.Euler(0, 0, 0));
        else if (Input.GetKeyDown(_downKey))
            _snakeHead.Rotate(Quaternion.Euler(0, 180, 0));
        else if (Input.GetKeyDown(_rightKey))
            _snakeHead.Rotate(Quaternion.Euler(0, 90, 0));
        else if (Input.GetKeyDown(_leftKey))
            _snakeHead.Rotate(Quaternion.Euler(0, -90, 0));
    }
}