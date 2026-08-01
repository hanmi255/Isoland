using Assets.Scripts.Utilities;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Inventory.UI
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void UpdateItemName(ItemName itemName)
        {
            _text.text = itemName switch
            {
                ItemName.Key => "信箱钥匙",
                ItemName.Ticket => "船票",
                _ => ""
            };
        }
    }
}