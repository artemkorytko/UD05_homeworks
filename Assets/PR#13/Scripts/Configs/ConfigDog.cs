using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ConfigDog", menuName = "Configs/ConfigDog", order = 0)]
    public class ConfigDog : ScriptableObject
    {
        [SerializeField] private int health = 10;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float delayBetweenAttack = 2f;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float rotateSpeed = 5f;

        public int Health => health;
        public float Speed => speed;
        public float DelayBetweenAttack => delayBetweenAttack;

        public float Radius => radius;

        public float RotateSpeed => rotateSpeed;
    }
}