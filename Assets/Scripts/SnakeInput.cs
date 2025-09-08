using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;

    private void Update()
    {
        if (Input.GetKeyDown(_leftKey) )
        {
            RotateLeft();
        }
        else if (Input.GetKeyDown(_rightKey))
        {
            RotateRight();
        }
    }

    private void RotateLeft()
    {
        _snakeHead.Rotate(_snakeHead.transform.rotation * Quaternion.Euler(0, -90, 0));
    }

    private void RotateRight()
    {
        _snakeHead.Rotate(_snakeHead.transform.rotation * Quaternion.Euler(0, 90, 0));
    }
}