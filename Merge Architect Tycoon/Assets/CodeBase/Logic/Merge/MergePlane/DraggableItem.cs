using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool isClicked = false;
    public Slot slot;
    public Image image;

    private Transform parentAfterDrag;
    private Vector3 startMousePosition;
    [Inject]
    private Canvas canvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.SlotState == SlotState.Draggable)
        {
            isClicked = false;
            if (slot != null)
            {
                slot = GetComponentInParent<Slot>();
            }

            parentAfterDrag = transform.parent;
            if (!slot.IsEmpty)
            {
                slot.OnClick();
                slot.DisableSelected();
                // if (Inp < 2)
                {
                    transform.SetParent(transform.parent.parent.parent);
                    transform.SetAsLastSibling();
                    image.raycastTarget = false;
                    startMousePosition 
                        = Input.mousePosition / canvas.scaleFactor - transform.localPosition;
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slot.SlotState == SlotState.Draggable)
        {
            if (!slot.IsEmpty)
            {
                transform.localPosition
                    = Input.mousePosition / canvas.scaleFactor - startMousePosition;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slot.SlotState == SlotState.Draggable)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
    }
}