using UnityEngine;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.Transition
{
    public class Teleport : MonoBehaviour
    {
        public SceneName toScene;

        public void OnTeleport()
        {
            SceneController.Instance.FadeAndLoadScene(toScene);
        }
    }
}
