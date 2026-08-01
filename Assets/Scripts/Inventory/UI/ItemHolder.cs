using Assets.Scripts.Inventory.Data;
using Assets.Scripts.Inventory.Logic;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.UI
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private SlotUI _slotUI;

        private int _currentItemIndex;

        private void OnEnable()
        {
            EventBus.UIUpdateEvent += OnUIUpdate;
        }

        private void OnDisable()
        {
            EventBus.UIUpdateEvent -= OnUIUpdate;
        }

        public void SwitchItem(int direction)
        {
            int newIndex = _currentItemIndex + direction;
            EventBus.CallOnItemSwitched(newIndex);
        }

        private void OnUIUpdate(ItemDetails itemDetails, int index)
        {
            if (itemDetails == null)
            {
                _currentItemIndex = -1;
                _slotUI.SetEmpty();
                _leftButton.interactable = false;
                _rightButton.interactable = false;
                return;
            }

            _currentItemIndex = index;
            _slotUI.SetItem(itemDetails);
            _leftButton.interactable = index > 0;
            _rightButton.interactable = index < InventoryManager.Instance.ItemCount - 1;
        }
    }
}
