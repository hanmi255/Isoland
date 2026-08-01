using UnityEngine;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.Inventory.Logic
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemName _itemName;

        public ItemName ItemName => _itemName;

        public void OnPickUp()
        {
            InventoryManager.Instance.AddItem(_itemName);
            gameObject.SetActive(false);
        }
    }
}
