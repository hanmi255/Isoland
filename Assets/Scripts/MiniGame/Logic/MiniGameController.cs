using Assets.Scripts.MiniGame.Data;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.MiniGame.Logic
{
    public class MiniGameController : SingletonMonoBehaviour<MiniGameController>
    {
        [SerializeField] private UnityEvent _onGameComplete;

        [Header("组件引用")]
        [SerializeField] private SO_MiniGameH2AData[] _dataList;
        [SerializeField] private GameObject _lineParent;
        [SerializeField] private LineRenderer _linePrefab;
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Transform[] _holes;

        private SO_MiniGameH2AData _data;

        private void OnEnable()
        {
            EventBus.CheckMiniGameCompletedEvent += OnCheckGameCompleted;
        }

        private void OnDisable()
        {
            EventBus.CheckMiniGameCompletedEvent -= OnCheckGameCompleted;
        }

        public void SetNewWeekData(int week)
        {
            _data = _dataList[week];
            DrawLine();
            CreateBall();
        }

        public void ResetGame()
        {
            // 删除 Ball 之后重新创建
            foreach (var ball in FindObjectsOfType<Ball>())
            {
                Destroy(ball.gameObject);
            }
            CreateBall();
        }

        private void DrawLine()
        {
            foreach (var connections in _data.lineConnections)
            {
                var line = Instantiate(_linePrefab, _lineParent.transform);
                line.SetPosition(0, _holes[connections.from].position);
                line.SetPosition(1, _holes[connections.to].position);

                // 创建每个 Hole 的 ConnectedHoles
                _holes[connections.from].GetComponent<Hole>().ConnectedHoles.Add(_holes[connections.to].GetComponent<Hole>());
                _holes[connections.to].GetComponent<Hole>().ConnectedHoles.Add(_holes[connections.from].GetComponent<Hole>());
            }
        }

        private void CreateBall()
        {
            for (int i = 0; i < _data.startOrder.Count; i++)
            {
                if (_data.startOrder[i] == BallName.None)
                {
                    _holes[i].GetComponent<Hole>().IsEmpty = true;
                    continue;
                }

                Ball ball = Instantiate(_ballPrefab, _holes[i]);
                _holes[i].GetComponent<Hole>().IsEmpty = false;
                ball.SetupBall(_data.GetBallDetails(_data.startOrder[i]));
                _holes[i].GetComponent<Hole>().CheckBall(ball);
            }
        }

        private void OnCheckGameCompleted()
        {
            // 检查是否所有 Ball 都在正确的 Hole 中
            foreach (var ball in FindObjectsOfType<Ball>())
            {
                if (!ball.IsMatch)
                    return;
            }

            // 游戏完成后禁用所有 Hole 的 碰撞体
            foreach (var hole in _holes)
            {
                hole.GetComponent<CircleCollider2D>().enabled = false;
            }

            EventBus.CallGameCompleted(_data.gameName);

            // 执行游戏完成事件 切换场景到 H2
            _onGameComplete?.Invoke();
        }
    }
}
