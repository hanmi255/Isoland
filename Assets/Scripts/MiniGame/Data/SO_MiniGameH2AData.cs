using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.MiniGame.Data
{
    [CreateAssetMenu(fileName = "SO_MiniGameH2AData", menuName = "SO/SO_MiniGameH2AData", order = 2)]
    public class SO_MiniGameH2AData : ScriptableObject
    {
        [Header("游戏名称")]
        public SceneName gameName;

        [Header("球的名称和对应图片")]
        public List<BallDetails> ballDetailsList;

        [Header("连接关系")]
        public List<Connections> lineConnections;
        public List<BallName> startOrder;

        public BallDetails GetBallDetails(BallName ballName)
        {
            return ballDetailsList.Find(ball => ball.ballName == ballName);
        }
    }

    [System.Serializable]
    public class BallDetails
    {
        public BallName ballName;
        public Sprite rightSprite;
        public Sprite wrongSprite;
    }

    [System.Serializable]
    public class Connections
    {
        public int from;
        public int to;
    }
}
