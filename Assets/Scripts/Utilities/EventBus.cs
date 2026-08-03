using System;
using Assets.Scripts.Dialogue.Data;
using Assets.Scripts.Inventory.Data;

namespace Assets.Scripts.Utilities
{
    public static class EventBus
    {
        // UI 更新事件
        public static event Action<ItemDetails,int> UIUpdateEvent;
        public static void CallUIUpdated(ItemDetails itemDetails, int index)
        {
            UIUpdateEvent?.Invoke(itemDetails, index);
        }

        // 场景卸载前后事件
        public static event Action BeforeSceneUnloadEvent;
        public static void CallBeforeSceneUnload()
        {
            BeforeSceneUnloadEvent?.Invoke();
        }
        
        public static event Action AfterSceneLoadEvent;
        public static void CallAfterSceneLoad()
        {
            AfterSceneLoadEvent?.Invoke();
        }

        // Slot 点击事件
        public static event Action<ItemDetails, bool> SlotSelectedEvent;
        public static void CallSlotSelected(ItemDetails itemDetails, bool isSelected)
        {
            SlotSelectedEvent?.Invoke(itemDetails, isSelected);
        }

        // Item 使用事件
        public static event Action<ItemName> ItemUsedEvent;
        public static void CallItemUsed(ItemName itemName)
        {
            ItemUsedEvent?.Invoke(itemName);
        }

        // Item 切换事件
        public static event Action<int> ItemSwitchedEvent;
        public static void CallItemSwitched(int index)
        {
            ItemSwitchedEvent?.Invoke(index);
        }

        // 对话事件
        public static event Action<DialogueLine> ShowDialogueEvent;
        public static void CallShowDialogue(DialogueLine dialogueLine)
        {
            ShowDialogueEvent?.Invoke(dialogueLine);
        }

        // 检查 MiniGame 是否完成事件
        public static event Action CheckMiniGameCompletedEvent;
        public static void CallCheckMiniGameCompleted()
        {
            CheckMiniGameCompletedEvent?.Invoke();
        }

        // MiniGame 完成事件
        public static event Action<SceneName> GameCompletedEvent;
        public static void CallGameCompleted(SceneName gameName)
        {
            GameCompletedEvent?.Invoke(gameName);
        }
    }
}
