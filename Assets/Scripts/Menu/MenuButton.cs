using Assets.Scripts.Transition;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MenuButton : MonoBehaviour
    {
        public void GoBackToMainMenu()
        {
            SceneController.Instance.FadeAndLoadScene(SceneName.MainMenu);

            // 保存当前游戏进度
        }
    }
}
