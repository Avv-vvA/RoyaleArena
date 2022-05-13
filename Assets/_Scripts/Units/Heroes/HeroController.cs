using _Scripts.Managers;
using _Scripts.Scriptables;
using Components;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Units.Hero
{
    public class HeroController : Components.Components
    {
        private Vector3 v_diff;
        private float atan2;
        private Vector3 nearestPosition;
        private float castleDelay;
        private Vector3 projectileSpawnPos;
        private NavMeshAgent agent;
        
        private void Update()
        {
            if (state == State.Move)
            {
                MoveToTarget();
            }
            else if (state == State.Attack)
            {
                Attacking();
            }
            else if (state == State.Stun)
            {
                Handler.StunEffect();
            }
            else if (state == State.Freeze)
            {
                agent.ResetPath();
                Handler.FreezEffect();
            }
        }
        
        private void Attacking()
        {
            if (Handler.IsDeath())
            {
                state = State.Death;
            }

            if (target == null || target.IsDeath())
            {
                FindTarget();
            }

            Rotation();

            attackDelayRestart -= Time.deltaTime;
            if (attackDelayRestart <= 0)
            {
                attackDelayRestart = Stats.FinalAttackDelay;
                if (HeroData.AttackType == AttackType.Melee)
                {
                    hittable.Hit(); // вызов метода каждого юнита 
                    Invoke("DealDmg", Stats.FinalAttackDelay / 2);
                }
                else
                {
                    if (target == null || (target.IsDeath())) return;
                    if (Vector3.Distance(target.GetPosition(), GetPosition()) < HeroData.BaseStats.attackRadius)
                    {
                        if (target == null || target.IsDeath()) FindTarget();
                        Invoke("ArrowSpawn", Stats.FinalAttackDelay / 2);
                        hittable.Hit(); // вызов метода каждого юнита 
                    }
                    else
                    {
                        FindTarget();
                    }
                }
            }
        }

        private void DealDmg()
        {
            if (target == null || target.IsDeath()) return;

            if (target.ApplyDamage(Stats.FinalDamage, Stats.FinalDmgRatio))
            {
                target = null;
                FindTarget();
            }
        } // нанесение урона ближнего боя после половины анимации атаки

        private void ArrowSpawn() // Выстрел после половины анимации атаки
        {
            var obj = Instantiate(HeroData.UnitsPf.projectilePf, _heroesPf.ProjectileSpawnPos.position, Quaternion.identity, t_move);
            obj.Setup(target, Stats.FinalDamage, Stats.FinalDmgRatio);
        }

        private void MoveToTarget()
        {
            if (!HeroData.Restrictions.canMove || state == State.Freeze) return;

            if (target != null)
            {
                if (target.IsDeath())
                {
                    FindTarget();
                }
                else
                {
                    if (Vector3.Distance(target.GetPosition(), GetPosition()) > HeroData.BaseStats.attackRadius)
                    {
                        Rotation();
                        agent.SetDestination(target.GetPosition());
                    }
                    else
                    {
                        agent.ResetPath();
                        SetState(State.Attack);
                    }
                }
            }
        }


        public void SetMergeLevel(int mergeLevel)
        {
            Stats.mergeLevel = mergeLevel;
            Stats.CardMergeUpdate();
            HeroesBar.SetMergeLevel(mergeLevel);
        }
        public void FindTarget()
        {
            if (Battlefield.Instance.HaveTarget(Category))
            {
                target = Battlefield.Instance.GetClosestTarget(GetPosition(), Category);
                if (HeroData.Restrictions.canMove)
                {
                    if (target == null)
                    {
                        agent.ResetPath();
                        SetState(State.Idle);
                        return;
                    }
                    
                    agent.SetDestination(target.GetPosition());
                    SetState(State.Move);
                    return;
                }
            }


            if (HeroData.Restrictions.canMove)
            {
                SetState(State.Idle);
            }
            
            
        }

        private void Rotation()
        {
            if (!HeroData.Restrictions.canMove) return;
            if (target == null || target.IsDeath()) return;
            v_diff = (target.GetPosition() - t_move.position);
            atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
            t_rotate.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 90);
        }
        
        public void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            transform.name = "" + HeroData.About.unitName;
            Handler.Initialize();
            var rnd = Random.Range(50, 55);
            agent.speed = HeroData.BaseStats.moveSpeed;
           
            agent.avoidancePriority = rnd;
           
            Invoke(nameof(FindTarget),0.1f);
            
          
        }
    }
}