using Interface;

namespace _Scripts.Units.Heroes.Types
{
    public class Bonny : IHittable
    {
        private readonly Components.Components components;
        public Bonny(Components.Components components)
        {
           this.components = components;
        }
        
        public void Hit()
        {
            components.Anim.PlayAttack();
        }
    }
}
