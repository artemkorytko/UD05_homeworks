using DG.Tweening;
using UnityEngine;

namespace Vikings.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform pointOne;
        [SerializeField] private Transform pointTwo;

        public void GoToPointOne()
        {
            var rot = (pointOne.transform.rotation).eulerAngles;
            transform.DOMove(pointOne.position, 2f, false);
            transform.DORotate(rot, 2f, RotateMode.Fast);
        }
        
        public void GoToPointTwo()
        {
            var rot = (pointTwo.transform.rotation).eulerAngles;
            transform.DOMove(pointTwo.position, 2f, false);
            transform.DORotate(rot, 2f, RotateMode.Fast);
        }
    }
}