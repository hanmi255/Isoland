using Assets.Scripts.Interactive;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.MiniGame.Logic
{
    public class H2AReset : InteractiveBase
    {
        [SerializeField] private Transform _gear;

        public override void EmptyClick()
        {
            // 绕z轴旋转180度
            _gear.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
            // 重置游戏
            MiniGameController.Instance.ResetGame();
        }
    }
}
