using Interface;
using UnityEngine;

namespace _Scripts.Units.Heroes.Types
{
    public class Gerra : IHittable
    {
        private readonly Components.Components components;

        private int rnd;
        public Gerra(Components.Components components)
        {
            this.components = components;
        }
        public void Hit()
        {
            rnd = Random.Range(0, 100);
            if (rnd <= components.Stats.FinalSkill1Values)
            {
                components.Anim.PlaySkill();
            }
            else
            {
                components.Anim.PlayAttack();
            }
        }
        
        
    }
}
