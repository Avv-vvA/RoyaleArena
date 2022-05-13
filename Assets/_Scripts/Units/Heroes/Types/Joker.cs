using Interface;

namespace _Scripts.Units.Heroes.Types
{
    public class Joker : IHittable
    {
        private readonly Components.Components components;
        public Joker(Components.Components components)
        {
            this.components = components;
        }

        public void Hit()
        {
            components.Anim.PlayAttack();
        }
    }
}
