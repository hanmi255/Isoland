using System.Collections.Generic;
using Assets.Scripts.Interactive;
using Assets.Scripts.Inventory.Data;
using Assets.Scripts.Inventory.Logic;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ObjectManager : MonoBehaviour
    {
        private readonly Dictionary<ItemName, bool> _itemAvailableDic = new();
        private readonly Dictionary<string, bool> _interactiveObjectUsedDic = new();

        private void OnEnable()
        {
            EventBus.BeforeSceneUnloadEvent += OnBeforeSceneUnload;
            EventBus.AfterSceneLoadEvent += OnAfterSceneLoad;
            EventBus.UIUpdateEvent += OnUIUpdate;
        }

        private void OnDisable()
        {
            EventBus.BeforeSceneUnloadEvent -= OnBeforeSceneUnload;
            EventBus.AfterSceneLoadEvent -= OnAfterSceneLoad;
            EventBus.UIUpdateEvent -= OnUIUpdate;
        }

        private void OnBeforeSceneUnload()
        {
            // 保存场景中物品的状态
            foreach (var item in FindObjectsOfType<Item>())
            {
                if (!_itemAvailableDic.ContainsKey(item.ItemName))
                {
                    _itemAvailableDic.Add(item.ItemName, true);
                }
            }

            // 保存场景中交互对象的状态
            foreach (var interactive in FindObjectsOfType<InteractiveBase>())
            {
                if (_interactiveObjectUsedDic.ContainsKey(interactive.name))
                {
                    _interactiveObjectUsedDic[interactive.name] = interactive.IsInteracted;
                }
                else
                {
                    _interactiveObjectUsedDic.Add(interactive.name, interactive.IsInteracted);
                }
            }
        }

        private void OnAfterSceneLoad()
        {
            // 恢复场景中物品的状态
            foreach (var item in FindObjectsOfType<Item>())
            {
                if (!_itemAvailableDic.ContainsKey(item.ItemName))
                {
                    _itemAvailableDic.Add(item.ItemName, true);
                }
                else
                {
                    item.gameObject.SetActive(_itemAvailableDic[item.ItemName]);
                }
            }

            // 恢复场景中交互对象的状态
            foreach (var interactive in FindObjectsOfType<InteractiveBase>())
            {
                if (_interactiveObjectUsedDic.ContainsKey(interactive.name))
                {
                    interactive.IsInteracted = _interactiveObjectUsedDic[interactive.name];
                }
                else
                {
                    _interactiveObjectUsedDic.Add(interactive.name, interactive.IsInteracted);
                }
            }
        }

        // 拾取物品后，将场景中的物体隐藏
        private void OnUIUpdate(ItemDetails itemDetails, int index)
        {
            if (itemDetails != null)
            {
                _itemAvailableDic[itemDetails.name] = false;
            }
        }
    }
}