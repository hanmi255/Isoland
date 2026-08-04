using System.Collections.Generic;
using Assets.Scripts.Interactive;
using Assets.Scripts.Inventory.Data;
using Assets.Scripts.Inventory.Logic;
using Assets.Scripts.MiniGame.Logic;
using Assets.Scripts.SaveLoadSystem;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ObjectManager : SingletonMonoBehaviour<ObjectManager>, ISaveable
    {
        private Dictionary<ItemName, bool> _itemAvailableDic = new();
        private Dictionary<string, bool> _interactiveObjectUsedDic = new();
        private Dictionary<SceneName, bool> _miniGamePassedDic = new();

        private int _currentWeek;
        private MiniGameController _currentMiniGameController;

        private void Start()
        {
            ISaveable saveable = this;
            saveable.SaveableRegister();
        }

        private void OnEnable()
        {
            EventBus.BeforeSceneUnloadEvent += OnBeforeSceneUnload;
            EventBus.AfterSceneLoadEvent += OnAfterSceneLoad;
            EventBus.UIUpdateEvent += OnUIUpdate;
            EventBus.GameCompletedEvent += OnGameCompleted;
            EventBus.NewWeekStartedEvent += OnNewWeekStarted;
        }

        private void OnDisable()
        {
            EventBus.BeforeSceneUnloadEvent -= OnBeforeSceneUnload;
            EventBus.AfterSceneLoadEvent -= OnAfterSceneLoad;
            EventBus.UIUpdateEvent -= OnUIUpdate;
            EventBus.GameCompletedEvent -= OnGameCompleted;
            EventBus.NewWeekStartedEvent -= OnNewWeekStarted;
        }


        #region ISaveable 接口实现

        public GameSaveData GenerateSaveData()
        {
            GameSaveData saveData = new()
            {
                currentWeek = _currentWeek,
                itemAvailableDic = _itemAvailableDic,
                interactiveObjectUsedDic = _interactiveObjectUsedDic,
                miniGamePassedDic = _miniGamePassedDic
            };
            return saveData;
        }

        public void RestoreGameData(GameSaveData saveData)
        {
            _currentWeek = saveData.currentWeek;
            _itemAvailableDic = saveData.itemAvailableDic;
            _interactiveObjectUsedDic = saveData.interactiveObjectUsedDic;
            _miniGamePassedDic = saveData.miniGamePassedDic;
        }

        #endregion

        #region Event
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

            // 保存场景中小游戏的状态
            foreach (var game in FindObjectsOfType<MiniGameEnter>())
            {
                if (_miniGamePassedDic.ContainsKey(game.GameName))
                {
                    _miniGamePassedDic[game.GameName] = game.IsCompleted;
                }
                else
                {
                    _miniGamePassedDic.Add(game.GameName, game.IsCompleted);
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

            // 恢复场景中小游戏的状态
            foreach (var game in FindObjectsOfType<MiniGameEnter>())
            {
                if (_miniGamePassedDic.TryGetValue(game.GameName, out bool isCompleted))
                {
                    game.IsCompleted = isCompleted;
                    game.UpdateMiniGameState();
                }
                else
                {
                    _miniGamePassedDic.Add(game.GameName, game.IsCompleted);
                }
            }

            // 设置当前小游戏控制器
            _currentMiniGameController = FindObjectOfType<MiniGameController>();
            _currentMiniGameController?.SetNewWeekData(_currentWeek);
        }

        // 拾取物品后，将场景中的物体隐藏
        private void OnUIUpdate(ItemDetails itemDetails, int index)
        {
            if (itemDetails != null)
            {
                _itemAvailableDic[itemDetails.name] = false;
            }
        }

        private void OnGameCompleted(SceneName gameName)
        {
            _miniGamePassedDic[gameName] = true;
        }

        private void OnNewWeekStarted(int week)
        {
            _currentWeek = week;
            // 清空所有状态，准备新一周
            _itemAvailableDic.Clear();
            _interactiveObjectUsedDic.Clear();
            _miniGamePassedDic.Clear();
        }

        #endregion
    }
}