using Assets.Scripts.Inventory.Data;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.UI
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private ToolTip _toolTip;
        private ItemDetails _currentItem;
        private bool _isSelected;

        public void SetItem(ItemDetails item)
        {
            _currentItem = item;
            gameObject.SetActive(true);
            _itemImage.sprite = item.icon;
            _itemImage.SetNativeSize();
        }

        public void SetEmpty()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _isSelected = !_isSelected;
            EventBus.CallSlotSelected(_currentItem, _isSelected);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (gameObject.activeInHierarchy)
            {
                _toolTip.gameObject.SetActive(true);
                _toolTip.UpdateItemName(_currentItem.name);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _toolTip.gameObject.SetActive(false);
        }
    }
}