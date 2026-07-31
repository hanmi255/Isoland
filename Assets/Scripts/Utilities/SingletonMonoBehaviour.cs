using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance; }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                // 只保留首个实例，避免场景中出现重复管理器。
                Destroy(gameObject);
            }
        }
    }
}
