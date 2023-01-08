using UnityEngine;

namespace Vikings_against_the_church.Scripts
{
    public class Viking : MonoBehaviour
    {
        [SerializeField] private string _name = "None";
        [SerializeField] private float _speed = 3f;
       
        
        private Vector3 _target;
        private Vector3 _firstPoint;

        private int _reward;
        private bool _isReturnInSpawnPiont;
        private bool _isAttackTower;
        
        public Vector3 FirstPoint => _firstPoint;
        public string Name => _name;
        public int Reward => _reward;
        public bool IsReturnInSpawnPiont => _isReturnInSpawnPiont;
        public bool IsAttackTower => _isAttackTower;

        private void Awake()
        {
            _firstPoint = transform.position;
        }

        private void Update()
        {
            if (transform.position != _target)
                Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Point>())
                _isAttackTower = true;
            
            if (other.TryGetComponent(out Tower tower))
                _target = tower.GetTrargetCoin().transform.position;

            if (other.TryGetComponent(out Coin coin))
            {
                _reward = coin.Reward;
                _isReturnInSpawnPiont = true;
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }

        public void SetTarget(Vector3 vector3)
        {
            _target = vector3;
        }
    }
}