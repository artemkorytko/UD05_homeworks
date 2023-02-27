using DG.Tweening;
using UnityEngine;

namespace Vikings.Scripts
{
    public class DraccarController : MonoBehaviour
    {
        public void Arrival(Vector3 point, float duration)
        {
            transform.DOMove(point, duration, false);
        }

        public void Departure(float duration)
        {
            var reverse = transform.rotation * new Vector3(0, -180, 0);
            transform.DORotate(reverse, 2f, RotateMode.Fast);
            transform.DOMoveX(transform.position.x - 30f, duration, false);
        }
    }
}