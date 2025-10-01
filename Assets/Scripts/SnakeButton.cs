using UnityEngine;
using UnityEngine.EventSystems;

public class SnakeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject snakeIcon;

    public void OnPointerEnter(PointerEventData eventData)
    {
        snakeIcon.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        snakeIcon.SetActive(false);
    }
}