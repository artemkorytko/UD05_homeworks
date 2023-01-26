using Configs;
using UnityEngine;

namespace Weapons.Base
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected int damage;
        public int Damage => damage;
    }
}