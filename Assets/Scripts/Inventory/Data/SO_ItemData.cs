using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Inventory.Data
{
    [CreateAssetMenu(fileName = "SO_ItemData", menuName = "SO/SO_ItemData", order = 0)]
    public class SO_ItemData : ScriptableObject
    {
        public List<ItemDetails> itemDetailsList;

        public ItemDetails GetItemDetails(ItemName name)
        {
            return itemDetailsList.Find(item => item.name == name);
        }
    }

    [System.Serializable]
    public class ItemDetails
    {
        public ItemName name;
        public Sprite icon;
    }
}