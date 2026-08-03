using System.Collections.Generic;
using Assets.Scripts.Interactive;
using Assets.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.MiniGame.Logic
{
    public class Hole : InteractiveBase
    {
        public bool IsEmpty { get; set; }
        public HashSet<Hole> ConnectedHoles = new();

        [SerializeField] private BallName _matchBallName;
        private Ball _currentBall;

        public void CheckBall(Ball ball)
        {
            _currentBall = ball;
            if (ball.BallName == _matchBallName)
            {
                _currentBall.IsMatch = true;
                _currentBall.SetRight();
            }
            else
            {
                _currentBall.IsMatch = false;
                _currentBall.SetWrong();
            }
        }

        public override void EmptyClick()
        {
            foreach (var hole in ConnectedHoles)
            {
                // 相连的 Hole 都没有空位，跳过
                if (!hole.IsEmpty) continue;

                // 移动球
                DOTween.To(() => _currentBall.transform.position, x => _currentBall.transform.position = x, hole.transform.position, 0.5f)
                    .OnComplete(() =>
                    {
                        _currentBall.transform.SetParent(hole.transform);
                        hole.CheckBall(_currentBall);
                        _currentBall = null;

                        // 改变状态
                        IsEmpty = true;
                        hole.IsEmpty = false;

                        // 每次移动后都检查是否完成
                        EventBus.CallCheckMiniGameCompleted();
                    });
            }
        }
    }
}
