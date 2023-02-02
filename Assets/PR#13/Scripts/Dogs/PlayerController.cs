using Cysharp.Threading.Tasks;
using DogOOP.Base;
using UnityEngine;

using Weapons.Base;

namespace DogOOP
{
    public class PlayerController : DogsBase
    {
        private float _rotateSpeed;
        private bool _isAttack;
        private float _counter;
        
        private Joystick _joystick;
        
        private void Awake()
        {
            base.Awake();
            _rotateSpeed = config.RotateSpeed;
            _joystick = FindObjectOfType<Joystick>();
        }
        
        
        protected override void Attack()
        {
            if (!_isAttack)
            {
                _isAttack = true;
                _animator.SetTrigger(Attack01);
            }
            else
            {
                _isAttack = false;
                _animator.SetTrigger(Attack02);
            }
        }

        protected override void DoAction()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _counter >= config.DelayBetweenAttack)
            {
                _counter = 0;
                Attack();
            }
            _counter += Time.deltaTime; 
            
            _animator.SetFloat(Speed, _joystick.Direction.magnitude);
            if (_joystick.Direction != Vector2.zero)
                Move();

        }

        private void Move()
        {
            var direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y); 
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotateSpeed * Time.deltaTime); 
            
            var magnitude = _joystick.Direction.magnitude; 
            transform.position += transform.forward * (config.Speed * Time.deltaTime * magnitude); 
        }
        
    
    }
}