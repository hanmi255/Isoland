using Assets.Scripts.Inventory.Logic;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class MailBox : InteractiveBase
    {
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        [SerializeField] private Sprite _openedSprite;
        [SerializeField] private Item _ticket;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            EventBus.AfterSceneLoadEvent += OnAfterSceneLoad;
        }

        private void OnDisable()
        {
            EventBus.AfterSceneLoadEvent -= OnAfterSceneLoad;
        }

        private void OnAfterSceneLoad()
        {
            if (!_isInteracted)
            {
                _ticket.gameObject.SetActive(false);
            }
            else
            {
                _spriteRenderer.sprite = _openedSprite;
                _collider.enabled = false;
            }
        }

        protected override void UseItem()
        {
            _spriteRenderer.sprite = _openedSprite;
            _collider.enabled = false;
            _ticket.gameObject.SetActive(true);
        }
    }
}
