using _Scripts.Managers;
using _Scripts.Scriptables;
using _Scripts.Units.HeroesUnits;
using Components;
using UnityEngine;

public class UnitController : UnitComponents
{
    private Vector3 v_diff;
    private float atan2;
    private Vector3 nearestPosition;
    private float castleDelay;


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
            if (UnitData.AttackType == AttackType.Melee)
            {
                hittable.Hit(); // вызов метода каждого юнита 
                Invoke("DealDmg", Stats.FinalAttackDelay / 2);
            }
            else
            {
                if (target == null || (target.IsDeath())) return;
                if (Vector3.Distance(target.GetPosition(), GetPosition()) < UnitData.BaseStats.attackRadius)
                {
                    if (target == null || target.IsDeath()) FindTarget();
                    hittable.Hit(); // вызов метода каждого юнита 
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
        var obj = Instantiate(UnitData.UnitsPf.projectilePf, transform.position, Quaternion.identity, t_move);
        obj.Setup(target, Stats.FinalDamage, Stats.FinalDmgRatio);
    }

    private void MoveToTarget()
    {
        if (!UnitData.Restrictions.canMove || state == State.Freeze) return;

        if (target == null) return;
        if (target.IsDeath())
        {
            FindTarget();
        }
        else
        {
            if (Vector3.Distance(target.GetPosition(), GetPosition()) > UnitData.BaseStats.attackRadius)
            {
                Rotation();
                t_move.position = Vector3.MoveTowards(t_move.position, target.GetPosition(),
                    UnitData.BaseStats.moveSpeed * Time.deltaTime);
            }
            else
            {
                SetState(State.Attack);
            }
        }
    }


    private void FindTarget()
    {
        if (Battlefield.Instance.HaveTarget(Category))
        {
            target = Battlefield.Instance.GetClosestTarget(GetPosition(), Category);
            if (UnitData.Restrictions.canMove)
            {
                SetState(State.Move);
                return;
            }
        }

        if (UnitData.Restrictions.canMove)
        {
            SetState(State.Idle);
        }
    }

    private void Rotation()
    {
        if (!UnitData.Restrictions.canMove) return;
        if (target == null || target.IsDeath()) return;
        v_diff = (target.GetPosition() - t_move.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        t_rotate.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 90);
    }

    private void Start()
    {
        Invoke("FindTarget", 0.2f);
        transform.name = "" + UnitData.About.unitName;

        if (Category == Category.Player)
        {
            Battlefield.Instance.playerUnits.Add(Handler);
        }
        else
        {
            Battlefield.Instance.enemyUnits.Add(Handler);
        }

        Stats.Initialize();
    }
}