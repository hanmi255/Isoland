using UnityEngine;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.Transition
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private SceneName _toScene;

        public void OnTeleport()
        {
            SceneController.Instance.FadeAndLoadScene(_toScene);
        }
    }
}
