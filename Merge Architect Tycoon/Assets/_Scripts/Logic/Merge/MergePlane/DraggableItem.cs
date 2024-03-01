using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Logic.Merge.MergePlane
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //, IPointerClickHandler, IPointerExitHandler
    {
        public Image image;
        public Slot slot;
        public bool isClicked = false;

        [Inject(Id = "TransformForInhandItem")]
        private RectTransform _playerHand;
        private Vector3 startMousePosition;
    
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (slot.SlotState != SlotState.Draggable
                && slot.SlotState != SlotState.Unloading)
                return;

            isClicked = false;
            if (slot == null)
                slot = GetComponentInParent<Slot>();

            if (slot.IsEmpty)
                return;

            slot.OnClick();
            slot.DisableSelected();
            transform.SetParent(_playerHand);

            image.raycastTarget = false;
            startMousePosition = Input.mousePosition - transform.localPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (slot.IsEmpty)
                return;

            if (slot.SlotState != SlotState.Draggable
                && slot.SlotState != SlotState.Unloading)
                return;

            transform.localPosition = Input.mousePosition - startMousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (slot.SlotState == SlotState.Draggable
                || slot.SlotState == SlotState.Unloading)
            {
                transform.SetParent(slot.transform);
            }

            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            image.raycastTarget = true;
        }
    }
}