using System;
using Assets.Scripts.Dialogue.Data;
using Assets.Scripts.Inventory.Data;

namespace Assets.Scripts.Utilities
{
    public static class EventBus
    {
        // UI 更新事件
        public static event Action<ItemDetails,int> UIUpdateEvent;
        public static void CallOnUIUpdate(ItemDetails itemDetails, int index)
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
        public static void CallOnSlotSelected(ItemDetails itemDetails, bool isSelected)
        {
            SlotSelectedEvent?.Invoke(itemDetails, isSelected);
        }

        // Item 使用事件
        public static event Action<ItemName> ItemUsedEvent;
        public static void CallOnItemUsed(ItemName itemName)
        {
            ItemUsedEvent?.Invoke(itemName);
        }

        // Item 切换事件
        public static event Action<int> ItemSwitchedEvent;
        public static void CallOnItemSwitched(int index)
        {
            ItemSwitchedEvent?.Invoke(index);
        }

        // 对话事件
        public static event Action<DialogueLine> ShowDialogueEvent;
        public static void CallOnShowDialogue(DialogueLine dialogueLine)
        {
            ShowDialogueEvent?.Invoke(dialogueLine);
        }
    }
}
