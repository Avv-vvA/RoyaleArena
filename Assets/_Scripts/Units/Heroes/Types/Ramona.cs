using Interface;
using UnityEngine;

namespace _Scripts.Units.Heroes.Types
{
    public class Ramona : IHittable
    {
        private readonly Components.Components components;
        private int rnd;
        public Ramona(Components.Components components)
        {
            this.components = components;
        }
        public void Hit()
        {
            rnd = Random.Range(0, 100);
            if (rnd <= components.Stats.FinalSkill1Values && components.target.CanSpelled())
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
