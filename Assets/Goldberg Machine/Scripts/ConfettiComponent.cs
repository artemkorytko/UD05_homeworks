using UnityEngine;

namespace Goldberg_Machine.Scripts
{
    public class ConfettiComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem confetti;

        public void ConfettiBurst()
        {
            confetti.Play();
        }
    }
}