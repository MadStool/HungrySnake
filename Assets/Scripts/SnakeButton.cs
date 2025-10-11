using UnityEngine;
using UnityEngine.EventSystems;

public class SnakeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform _snakeIcon;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_snakeIcon != null)
            _snakeIcon.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_snakeIcon != null)
            _snakeIcon.gameObject.SetActive(false);
    }
}