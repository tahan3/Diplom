using UnityEngine;

namespace Damage
{
    public abstract class AShooter : MonoBehaviour, IShoot
    {
        protected float reloadDuration;

        public abstract void Shoot();
    }
}
