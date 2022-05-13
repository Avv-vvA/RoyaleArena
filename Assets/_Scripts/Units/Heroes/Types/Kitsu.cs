using Interface;

namespace _Scripts.Units.Heroes.Types
{
    public class Kitsu : IHittable
    {
        private readonly Components.Components components;
        public Kitsu(Components.Components components)
        {
            this.components = components;
        }
        
        public void Hit()
        {
            components.Anim.PlayAttack();
        }
    }
}