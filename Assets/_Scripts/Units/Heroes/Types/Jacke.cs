using Components;
using Interface;
using UnityEngine;

namespace _Scripts.Units.Heroes.Types
{
    public class Jacke : MonoBehaviour, IHittable
    {
        private Components.Components components;
        private IDamageable target;
        private GameObject wolfPf;
        private Vector3 spawnPos;
        private float timer = 1f;

        private void Awake()
        {
            components = GetComponent<Components.Components>();
            wolfPf = components.HeroData.UnitsPf.skill1Pf;
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

        private void SkillUse()
        {
            components.SetState(State.Skill);

            var rnd = Random.value;

            if (rnd >= 0.5)
            {
                spawnPos = new Vector3(components.t_move.position.x + 7 - rnd, components.t_move.position.y,
                    components.t_move.position.z);
            }
            else
            {
                spawnPos = new Vector3(components.t_move.position.x - 7 + rnd, components.t_move.position.y,
                    components.t_move.position.z);
            }


            Invoke(nameof(CreateWolf), components.Stats.FinalAttackAnimationSpeed / 2);
            Invoke("Skilled", components.Stats.FinalAttackAnimationSpeed);
        }

        private void CreateWolf()
        {
            var obj = Instantiate(components.HeroData.UnitsPf.skill1Pf, spawnPos, components.t_rotate.rotation);
            obj.GetComponent<UnitController>().Stats.WolfStats(components.Stats);
            
            obj.GetComponent<UnitController>().Category = components.Category;
        }

        public void Hit()
        {
            components.Anim.PlayAttack();
        }
    }
}