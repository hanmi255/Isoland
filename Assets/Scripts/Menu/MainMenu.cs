using Assets.Scripts.SaveLoadSystem;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }

        public void ContinueGame()
        {
            // 读取当前游戏进度
            SaveLoadManager.Instance.Load();
        }

        public void StartNewWeekGame(int week)
        {
            EventBus.CallNewWeekStarted(week);
        }
    }
}
