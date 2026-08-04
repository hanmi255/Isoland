using System.Collections.Generic;
using Assets.Scripts.Inventory.Data;
using Assets.Scripts.SaveLoadSystem;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Inventory.Logic
{
    public class InventoryManager : SingletonMonoBehaviour<InventoryManager>, ISaveable
    {
        [Header("References")]
        [SerializeField] private SO_ItemData _itemData;
        private List<ItemName> _itemList = new();

        public int ItemCount => _itemList.Count;

        private void Start()
        {
            ISaveable saveable = this;
            saveable.SaveableRegister();
        }

        private void OnEnable()
        {
            EventBus.ItemUsedEvent += OnItemUsed;
            EventBus.ItemSwitchedEvent += OnItemSwitched;
            EventBus.AfterSceneLoadEvent += OnAfterSceneLoad;
            EventBus.NewWeekStartedEvent += OnNewWeekStarted;
        }

        private void OnDisable()
        {
            EventBus.ItemUsedEvent -= OnItemUsed;
            EventBus.ItemSwitchedEvent -= OnItemSwitched;
            EventBus.AfterSceneLoadEvent -= OnAfterSceneLoad;
            EventBus.NewWeekStartedEvent -= OnNewWeekStarted;
        }

        public void AddItem(ItemName itemName)
        {
            if (_itemList.Contains(itemName))
                return;

            _itemList.Add(itemName);
            int index = _itemList.IndexOf(itemName);

            EventBus.CallUIUpdated(_itemData.GetItemDetails(itemName), index);
        }

        #region ISaveable 接口实现

        public GameSaveData GenerateSaveData()
        {
            GameSaveData saveData = new()
            {
                itemList = _itemList
            };
            return saveData;
        }

        public void RestoreGameData(GameSaveData saveData)
        {
            _itemList = saveData.itemList;
        }

        #endregion

        #region Event

        private void OnItemUsed(ItemName itemName)
        {
            int index = _itemList.IndexOf(itemName);
            _itemList.RemoveAt(index);

            if (_itemList.Count == 0)
            {
                EventBus.CallUIUpdated(null, -1);
            }
            else
            {
                int newIndex = Mathf.Clamp(index, 0, _itemList.Count - 1);
                EventBus.CallUIUpdated(_itemData.GetItemDetails(_itemList[newIndex]), newIndex);
            }
        }

        private void OnItemSwitched(int index)
        {
            if (index < 0 || index >= _itemList.Count)
                return;

            ItemName itemName = _itemList[index];
            EventBus.CallUIUpdated(_itemData.GetItemDetails(itemName), index);
        }

        private void OnAfterSceneLoad()
        {
            if (_itemList.Count == 0)
            {
                EventBus.CallUIUpdated(null, -1);
            }
            else
            {
                EventBus.CallUIUpdated(_itemData.GetItemDetails(_itemList[0]), 0);
            }
        }

        private void OnNewWeekStarted(int week)
        {
            // 清空背包，准备新一周
            _itemList.Clear();
            EventBus.CallUIUpdated(null, -1);
        }

        #endregion
    }
}