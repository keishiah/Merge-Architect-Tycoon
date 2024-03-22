using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class DraggableItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler , IPointerDownHandler//, IPointerExitHandler
{
    public Image image;
    public Slot slot;
    public bool isClicked = false;

    [Inject(Id = "TransformForInhandItem")]
    private RectTransform _playerHand;
    [Inject]
    private Canvas _canvas;
    private Vector3 startMousePosition;


    public void OnPointerDown(PointerEventData eventData)
    {
        startMousePosition = Input.mousePosition / _canvas.scaleFactor - _playerHand.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.SlotState != SlotState.Draggable
            && slot.SlotState != SlotState.Unloading)
        {
            eventData.pointerDrag = null;
            return;
        }

        isClicked = false;
        if (slot == null)
            slot = GetComponentInParent<Slot>();

        if (slot.IsEmpty)
            return;

        slot.OnClick();
        slot.DisableSelected();
        transform.SetParent(_playerHand);

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slot.IsEmpty)
            return;

        if (slot.SlotState != SlotState.Draggable
            && slot.SlotState != SlotState.Unloading)
            return;

        _playerHand.localPosition = Input.mousePosition / _canvas.scaleFactor - startMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slot.SlotState == SlotState.Draggable
            || slot.SlotState == SlotState.Unloading)
        {
            transform.SetParent(slot.transform);
        }

        _playerHand.localPosition = Vector3.zero;
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        image.raycastTarget = true;
    }

}