using UnityEngine;

namespace Interface
{
    public interface IDamageable
    {
        public bool ApplyDamage(float dmgCount, float attackRatio);

        public bool IsDeath();

        public void Stunning(float stunTime);

        public void Freezing(float freezeTime, Animator animator);
        public bool CanSpelled();
        public Vector3 GetPosition();

        public Transform GetRotation();

        public void SetBuff(BuffType buffType, float value );
        
        public void RemoveBuff(BuffType buffType, float value );

    }
}
