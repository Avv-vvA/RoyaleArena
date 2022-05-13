using _Scripts.Managers;
using Components;
using Interface;
using UnityEngine;

namespace _Scripts.Units.Heroes.Types
{
    public class Skadi : MonoBehaviour, IHittable
    {
        private Components.Components components;

        private IDamageable target;
        public GameObject iceEffect;
        private Animator iceAnim;
        private float timer = 0.5f;

        
        private void Awake()
        {
            components = GetComponent<Components.Components>();
            iceEffect = Instantiate(components.HeroData.UnitsPf.skill1Pf,transform.parent);
            iceAnim = iceEffect.GetComponentInChildren<Animator>();
            iceEffect.SetActive(false);
        }


        private void SkillUse()
        {
            if (!Battlefield.Instance.HaveTarget(components.Category)) return;
            target = Battlefield.Instance.GetRandomTarget(components.Category);
            if (!target.CanSpelled()) return;
            components.SetState(State.Skill);
            target.Freezing(components.Stats.FinalSkill2Values, iceAnim);
            iceEffect.transform.position = target.GetPosition();
            iceEffect.transform.rotation = target.GetRotation().rotation;
            iceEffect.SetActive(true);
            Invoke("Unfreeze", components.Stats.FinalSkill2Values - 0.78f);
            Invoke("Skilled", components.Stats.FinalAttackAnimationSpeed - 0.5f);
        }


        private void FixedUpdate()
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && components.canUseSpell && !components.Handler.IsDeath())
            {
                timer = components.Stats.FinalSkill1Values;
                SkillUse();
            }
        }

        private void Skilled()
        {
            components.ReturnToCurrState();
        }

        private void Unfreeze()
        {
            iceAnim.SetTrigger("Off");
            Invoke("SkillOf", 1.5f);
        }

        private void SkillOf()
        {
            iceEffect.gameObject.SetActive(false);
        }

        public void Hit()
        {
            components.Anim.PlayAttack();
        }
    }
}