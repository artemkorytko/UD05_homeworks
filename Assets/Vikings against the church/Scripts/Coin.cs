using UnityEngine;
using DG.Tweening;

namespace Vikings_against_the_church.Scripts
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _reward;
        private Sequence _sequence;

        public int Reward => _reward;

        private void Start()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DORotate(new Vector3(90, 360, 0), 3f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));
            _sequence.SetLoops(-1);
        }

        private void OnDisable()
        {
            _sequence?.Kill(true);
        }
    }
}