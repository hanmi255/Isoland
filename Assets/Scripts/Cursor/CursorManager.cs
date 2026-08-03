using Assets.Scripts.Interactive;
using Assets.Scripts.Inventory.Data;
using Assets.Scripts.Inventory.Logic;
using Assets.Scripts.Transition;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Cursor
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _hand;

        private Vector3 MouseWorldPos =>
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private bool _canClick;
        private ItemName _currentItem;
        private bool _isHoldingItem;

        private void OnEnable()
        {
            EventBus.SlotSelectedEvent += OnSlotSelected;
            EventBus.ItemUsedEvent += OnItemUsed;
        }

        private void Update()
        {
            _canClick = GetMouseOverCollider() != null;

            if (_hand.gameObject.activeInHierarchy)
            {
                _hand.position = Input.mousePosition;
            }

            // 如果鼠标在UI上，不处理点击
            if (IsMouseOverUI())
                return;

            if (_canClick && Input.GetMouseButtonDown(0))
            {
                // 处理点击事件
                var collider = GetMouseOverCollider();
                if (collider != null)
                {
                    OnClick(collider.gameObject);
                }
            }
        }

        private void OnDisable()
        {
            EventBus.SlotSelectedEvent -= OnSlotSelected;
            EventBus.ItemUsedEvent -= OnItemUsed;
        }

        private void OnClick(GameObject obj)
        {
            switch (obj.tag)
            {
                case "Teleport":
                    // 处理传送点击
                    var teleport = obj.GetComponent<Teleport>();
                    teleport.OnTeleport();
                    break;
                case "Item":
                    // 处理物品点击
                    var item = obj.GetComponent<Item>();
                    item.OnPickUp();
                    break;
                case "Interactive":
                    // 处理交互点击
                    var interactive = obj.GetComponent<InteractiveBase>();
                    if (_isHoldingItem)
                        interactive.Interact(_currentItem);
                    else
                        interactive.EmptyClick();
                    break;
            }
        }

        private void OnSlotSelected(ItemDetails item, bool isSelected)
        {
            _isHoldingItem = isSelected;
            if (isSelected)
            {
                _currentItem = item.name;
            }
            _hand.gameObject.SetActive(_isHoldingItem);
        }

        private void OnItemUsed(ItemName itemName)
        {
            _currentItem = ItemName.None;
            _isHoldingItem = false;
            _hand.gameObject.SetActive(false);
        }

        // 获取鼠标点击位置的碰撞体
        private Collider2D GetMouseOverCollider()
        {
            return Physics2D.OverlapPoint(MouseWorldPos);
        }

        // 检查鼠标是否在UI上
        private bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
