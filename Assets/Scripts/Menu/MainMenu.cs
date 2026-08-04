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

        //TODO: 继续游戏
        public void ContinueGame()
        {

        }

        public void StartNewWeekGame(int week)
        {
            EventBus.CallNewWeekStarted(week);
        }
    }
}
