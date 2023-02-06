using UnityEngine;


namespace Vikings_against_the_church.Scripts
{
    public class VikingController : MonoBehaviour
    {
        [SerializeField] private string name;
        [SerializeField] private Transform model;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float speedLerp = 0.5f;

        private Transform _target;
        private Transform _firstPoint;
        private Quaternion _lookRotation;
        
        private Animator _animator;
        
        private int _reward;
        private bool _isReturnInSpawnPiont;
        private bool _isAttackTower;
        
        public string Name => name;
        public int Reward => _reward;
        public bool IsReturnInSpawnPiont => _isReturnInSpawnPiont;
        public bool IsAttackTower => _isAttackTower;

        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void Update()
        {
            if (transform.position == _target.position)
                return;
            
            Rotate();
            Move();
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Point>() && Mathf.Abs(transform.position.z - _target.position.z) < 0.3f) 
            {
                _isAttackTower = true;
                _animator.SetTrigger(Idle);
            }

            if (other.TryGetComponent(out Tower tower))
            {
                _animator.SetTrigger(Attack);
                _target = transform;
                
                var a = await tower.GetTrargetCoin();
                SetTarget(a);
            }
            
            if (other.TryGetComponent(out CoinPoint coinPoint) && Mathf.Abs(transform.position.z - _target.position.z) < 0.3f)
            {
                _reward = coinPoint.Reward;
                _isReturnInSpawnPiont = true;
                _animator.SetTrigger(Idle);
            }
            
            if (other.TryGetComponent(out Coin coin))
                coin.gameObject.SetActive(false);
        }
        
        public void SetTarget(Transform transformTarget)
        {
            _target = transformTarget;
            _animator.SetTrigger(Walk);
        }

        private void Move()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
        }

        private void Rotate()
        {
            //   моментальный поворот // 
            // var relativePos = _target.position - _model.position; // определяем направление поворота 
            // var rotate = Quaternion.LookRotation(relativePos); // Вычисляем Quaternion 
            // _model.localRotation = Quaternion.Euler(rotate.eulerAngles); // устанавливаем Quaternion и перед этим переводим его в угол euler

            // плавный поворот //
            var relativePos = _target.position - model.position; // определяем направление поворота 
            var rotation = Quaternion.LookRotation(relativePos, Vector3.up);// Вычисляем Quaternion, Vector3.up - это вокруг какой оси делать действие (это ось Y)
            var quaternion = Quaternion.Lerp(model.localRotation, rotation,speedLerp * Time.deltaTime); // Плавный поворот от Quaternion до Вычисляем Quaternion   
            model.localRotation = Quaternion.Euler(quaternion.eulerAngles); // устанавливаем Quaternion и перед этим переводим его в угол euler
            
                // при присвоении в модель нужно Quaternion перреводить в eulerAngles!!!!!!!!
        }
    }
}