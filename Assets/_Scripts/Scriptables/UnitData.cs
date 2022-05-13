using _Scripts.Scriptables;
using UnityEngine;


[CreateAssetMenu(fileName = "New enemy", order = 51)]
public class UnitData : UnitBase
{
    [SerializeField] private HeroRare heroRare;
    [SerializeField] private AttackType attackType;
    
    
    public AttackType AttackType => attackType;
    public HeroRare HeroRare => heroRare;
    
    
    
}
