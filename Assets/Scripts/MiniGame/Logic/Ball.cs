using Assets.Scripts.MiniGame.Data;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.MiniGame.Logic
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private BallDetails _ballDetails;
        public bool IsMatch { get; set; }
        public BallName BallName => _ballDetails.ballName;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetupBall(BallDetails ballDetails)
        {
            _ballDetails = ballDetails;
        }

        public void SetRight()
        {
            _spriteRenderer.sprite = _ballDetails.rightSprite;
        }

        public void SetWrong()
        {
            _spriteRenderer.sprite = _ballDetails.wrongSprite;
        }
    }
}
