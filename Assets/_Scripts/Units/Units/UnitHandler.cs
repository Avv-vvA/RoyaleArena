using _Scripts.Units.HeroesUnits;
using Components;
using DG.Tweening;
using Interface;
using UnityEngine;

namespace _Scripts.Units.Units
{
    public class UnitHandler : IDamageable
    {
        private EnemyController enemyController;
        private UnitComponents unitComponents;
        private UnitBar unitBar;

        private bool isDeath;
        private float dmgCount;
        private float startStunTimer;
        private float stunTimer;
        private float freezeTimer;
        private Animator freezAnim;

        private bool isShaked;
        private bool isEnemyCtr;

        public UnitHandler(EnemyController enemyController, UnitBar unitBar)
        {
            this.enemyController = enemyController;
            this.unitBar = unitBar;
            isEnemyCtr = true;
            this.unitBar.hpFill.fillAmount =
                enemyController.currHitPoints / enemyController.UnitData.BaseStats.maxHitPoints;
        }

        public UnitHandler(UnitComponents unitComponents)
        {
            this.unitComponents = unitComponents;
        }

        public bool ApplyDamage(float dmgCome, float attackRatio)
        {
            if (isDeath) return true;
            dmgCount = DmgCalculation(dmgCome, attackRatio);

            if (isEnemyCtr)
            {
                if (!isShaked)
                {
                    isShaked = true;
                    enemyController.t.DOShakeScale(0.2f, 0.3f).OnComplete(() => { isShaked = false; });
                }
                
                if (dmgCount >= enemyController.currHitPoints)
                {
                    enemyController.currHitPoints -= dmgCount;
                    unitBar.hpFill.fillAmount =
                        enemyController.currHitPoints / enemyController.UnitData.BaseStats.maxHitPoints;
                    isDeath = true;
                    return true;
                }

                enemyController.currHitPoints -= dmgCount;
                unitBar.hpFill.fillAmount =
                    enemyController.currHitPoints / enemyController.UnitData.BaseStats.maxHitPoints;
                return false;
            }


            if (dmgCount >= unitComponents.Stats.currHitPoints)
            {
                unitComponents.Stats.currHitPoints -= dmgCount;
                isDeath = true;
                return true;
            }

            unitComponents.Stats.currHitPoints -= dmgCount;

            return false;
        }

        public bool IsDeath()
        {
            return isDeath;
        }

        #region Stunning Effects

        public void Stunning(float stunTime)
        {
            startStunTimer = stunTime;
            stunTimer = stunTime;
            unitBar.stunFill.fillAmount = stunTimer / startStunTimer;
            unitBar.stunBar.SetActive(true);
            if (isDeath) return;
            enemyController.SetState(State.Stun);
        }

        public void StunEffect()
        {
            if (isDeath)
            {
                return;
            }

            stunTimer -= Time.deltaTime;
            unitBar.stunFill.fillAmount = stunTimer / startStunTimer;

            if (stunTimer <= 0)
            {
                unitBar.stunBar.SetActive(false);
                enemyController.SetState(State.Move);
            }
        }

        #endregion

        #region Freezing Effects

        public void Freezing(float freezeTime, Animator animator)
        {
            freezAnim = animator;
            freezeTimer = freezeTime;
            enemyController.SetState(State.Freeze);
        }

        public void FreezEffect()
        {
            if (isDeath) return;
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                enemyController.SetState(State.Move);
            }
        }

        #endregion


        public bool CanSpelled()
        {
            return isEnemyCtr
                ? enemyController.UnitData.Restrictions.canSpelled
                : unitComponents.UnitData.Restrictions.canSpelled;
        }

        public Vector3 GetPosition()
        {
            return isEnemyCtr ? enemyController.GetPosition() : unitComponents.GetPosition();
        }

        public Transform GetRotation()
        {
            return isEnemyCtr ? enemyController.GetRotation() : unitComponents.GetRotation();
        }

        public void SetBuff(BuffType buffType, float value)
        {
           
        }

        public void RemoveBuff(BuffType buffType, float value)
        {
            
        }


        private float DmgCalculation(float dmgCm, float attackRatio)
        {
            return dmgCm * attackRatio *
                   (1 - (enemyController.UnitData.BaseStats.armor * enemyController.FinalDefRatio) /
                       (1 + enemyController.UnitData.BaseStats.armor * enemyController.UnitData.BaseStats.armor));
        } // Считаем нанесенный урон после защиты
    }
}