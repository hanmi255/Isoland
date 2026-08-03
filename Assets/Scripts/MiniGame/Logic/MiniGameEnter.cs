using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.MiniGame.Logic
{
    public class MiniGameEnter : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onGamePassed;
        [SerializeField] private SceneName _gameName;
        private bool _isCompleted;

        public SceneName GameName => _gameName;
        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }

        // 通过 UnityEvent 启动 TeleportToH3, 禁用当前的碰撞体和SpriteRenderer 
        public void UpdateMiniGameState()
        {
            if (_isCompleted)
            {
                _onGamePassed?.Invoke();
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
