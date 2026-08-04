using System.Collections.Generic;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.SaveLoadSystem
{
    public class GameSaveData
    {
        public int currentWeek;                                   // 当前周数
        public SceneName currentScene;                            // 当前场景
        public Dictionary<ItemName, bool> itemAvailableDic;       // 物品可用状态字典
        public Dictionary<string, bool> interactiveObjectUsedDic; // 交互对象使用状态字典
        public Dictionary<SceneName, bool> miniGamePassedDic;     // 小游戏通过状态字典
        public List<ItemName> itemList;                           // 物品列表
    }
}
