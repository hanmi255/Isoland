using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class InteractiveBase : MonoBehaviour
    {
        [SerializeField] private ItemName _requiredItem;
        protected bool _isInteracted = false;

        public bool IsInteracted { get => _isInteracted; set => _isInteracted = value; }

        public void Interact(ItemName itemName)
        {
            if (itemName == _requiredItem && !_isInteracted)
            {
                _isInteracted = true;
                UseItem();
                EventBus.CallItemUsed(_requiredItem);
            }
        }

        protected virtual void UseItem() { }

        public virtual void EmptyClick()
        {
            Debug.Log("空点");
        }
    }
}
