using _Scripts.Units.HeroesUnits;
using Interface;

public class Wolf : IHittable
{
    private readonly UnitComponents components;
    public Wolf(UnitComponents components)
    {
        this.components = components;
    }

    public void Hit()
    {
       components.Anim.PlayAttack();
    }
}
