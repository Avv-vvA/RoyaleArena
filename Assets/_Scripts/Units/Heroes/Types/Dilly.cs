using _Scripts.Managers;
using Components;
using Interface;
using UnityEngine;

public class Dilly : MonoBehaviour, IHittable
{
    private Battlefield battlefield;
    private Components.Components components;

    private void Start()
    {
        components = transform.GetComponent<Components.Components>();
        battlefield = Battlefield.Instance;
        components.Handler.OnHeroIsDeath += RemoveBuff;
        Invoke(nameof(SetBuff), 0.2f);
    }


    private void RemoveBuff()
    {
        if (components.Category == Category.Player)
        {
            foreach (var t in battlefield.playerUnits)
            {
                if (!t.IsDeath())
                {
                    t.RemoveBuff(BuffType.AttackSpeed, components.Stats.FinalSkill1Values);
                }
            }
        }
        else
        {
            foreach (var t in battlefield.enemyUnits)
            {
                if (!t.IsDeath())
                {
                    t.RemoveBuff(BuffType.AttackSpeed, components.Stats.FinalSkill1Values);
                }
            }
        }
    }

    private void SetBuff()
    {
        if (components.Category == Category.Player)
        {
            foreach (var t in battlefield.playerUnits)
            {
                if (!t.IsDeath())
                {
                    t.SetBuff(BuffType.AttackSpeed, components.Stats.FinalSkill1Values);
                }
            }
        }
        else
        {
            foreach (var t in battlefield.enemyUnits)
            {
                if (!t.IsDeath())
                {
                    t.SetBuff(BuffType.AttackSpeed, components.Stats.FinalSkill1Values);
                }
            }
        }
        
       
    }

    public void Hit()
    {
        components.Anim.PlayAttack();
    }
}