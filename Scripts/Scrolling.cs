using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Scrolling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textObject;
    public float scrollSpeed;
    public float scrollAmount;
    private Vector3 startPosition;
    private bool isHovering;

    void Start()
    {
        startPosition = textObject.transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    void OnMouseScroll(PointerEventData eventData)
    {
        if (isHovering)
        {
            float scrollValue = -eventData.delta.y;
            Scroll(scrollValue * scrollAmount);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Scroll(-scrollAmount);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Scroll(scrollAmount);
        }
    }

    void Scroll(float amount)
    {
        Vector3 newPosition = textObject.transform.position;
        newPosition.y += amount;

        if (newPosition.y > startPosition.y + textObject.preferredHeight)
        {
            newPosition.y = startPosition.y + textObject.preferredHeight;
        }
        else if (newPosition.y < startPosition.y)
        {
            newPosition.y = startPosition.y;
        }

        textObject.transform.position = newPosition;
    }

}
