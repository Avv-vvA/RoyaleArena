using _Scripts.Managers;
using Interface;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Components
{
    public class Handler : IDamageable
    {
        private readonly Components components;
        private readonly Stats stats;


        public Handler(Components components, Stats stats)
        {
            this.components = components;
            this.stats = stats;
        }

        private bool isDeath;
        private bool isFreezing;
        private float dmgCount;
        private float startStunTimer;
        private float stunTimer;
        private float freezeTimer;
        private Animator freezAnim;
        private static readonly int Off = Animator.StringToHash("Off");

        public delegate void HeroIsDeath();

        public event HeroIsDeath OnHeroIsDeath;

        public void Initialize()
        {
            components.HeroesBar.HpChanger(components.Stats.currHitPoints, components.Stats.FinalMaxHitPoints);
            components.HeroesBar.SetHeroLevel(components.HeroData.cardLevel);
        }


        public bool ApplyDamage(float dmgCome, float attackRatio)
        {
            if (isDeath) return true;

            dmgCount = DmgCalculation(dmgCome, attackRatio);

            if (dmgCount >= stats.currHitPoints)
            {
                stats.currHitPoints -= dmgCount;

                if (GameManager.Instance.CharactersBar.PopupDamageText && components.HeroData.Restrictions.haveUiBar)
                {
                    // components.uiBar.PopupTextShow(components.category, dmgCount);
                }

                if (components.HeroData.Restrictions.haveUiBar)
                {
                    components.HeroesBar.HpChanger(stats.currHitPoints, stats.FinalMaxHitPoints);
                    components.HeroesBar.gameObject.SetActive(false);
                }

                Death();
                isDeath = true;
                return true;
            }

            stats.currHitPoints -= dmgCount;
            if (GameManager.Instance.CharactersBar.PopupDamageText && components.HeroData.Restrictions.haveUiBar)
            {
                //     components.uiBar.PopupTextShow(components.category, dmgCount);
            }

            if (components.HeroData.Restrictions.haveUiBar)
                components.HeroesBar.HpChanger(stats.currHitPoints, stats.FinalMaxHitPoints);
            return false;
        }


        #region Freezing Effects

        public void Freezing(float freezeTime, Animator animator)
        {
            isFreezing = true;
            freezAnim = animator;
            freezeTimer = freezeTime;
            components.SetState(State.Freeze);
        }

        public void FreezEffect()
        {
            if (isDeath) return;
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                isFreezing = false;
                components.SetState(State.Move);
            }
        }

        #endregion


        #region Stunning Effects

        public void Stunning(float stunTime)
        {
            startStunTimer = stunTime;
            stunTimer = stunTime;
            if (isDeath) return;
            components.SetState(State.Stun);
        }

        public void StunEffect()
        {
            if (!components.HeroData.Restrictions.haveUiBar) return;
            if (isDeath)
            {
                components.HeroesBar.StunBarChanger(0, 0);
                return;
            }

            stunTimer -= Time.deltaTime;
            components.HeroesBar.StunBarChanger(stunTimer, startStunTimer);
            if (stunTimer <= 0)
            {
                components.ReturnToCurrState();
            }
        }

        #endregion


        public bool CanSpelled()
        {
            return components.HeroData.Restrictions.canSpelled;
        }

        private void Death()
        {
            if (isFreezing)
            {
                if (freezAnim == null) return;
                freezAnim.SetTrigger(Off);
            }

            OnHeroIsDeath?.Invoke();

            components.Anim.PlayDeath();
            if (components.HeroData.Restrictions.haveUiBar)
            {
                components.HeroesBar.HideBar();
            }

            components.HideCharacter(1.5f);
            if (components.Category == Category.Player)
            {
                Battlefield.Instance.playerUnits.Remove(this);
            }
            else
            {
                Battlefield.Instance.enemyUnits.Remove(this);
            }
        }


        public bool IsDeath()
        {
            return isDeath;
        }

        public Vector3 GetPosition()
        {
            return components.GetPosition();
        }

        public Transform GetRotation()
        {
            return components.GetRotation();
        }

        public void SetBuff(BuffType buffType, float value)
        {
            stats.SetBuff(buffType, value);
        }

        public void RemoveBuff(BuffType buffType, float value)
        {
            stats.RemoveBuff(buffType, value);
        }


        private float DmgCalculation(float dmgCm, float attackRatio)
        {
            return dmgCm * attackRatio *
                   (1 - (stats.FinalArmor * stats.FinalDefRatio) / (1 + stats.FinalArmor * stats.FinalArmor));
        } // Считаем нанесенный урон после защиты
    }
}