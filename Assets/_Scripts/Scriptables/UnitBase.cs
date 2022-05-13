using System;
using Components;
using UnityEngine;
using Spine.Unity;

// ReSharper disable once CheckNamespace
public class UnitBase : ScriptableObject
{
    [SerializeField] private About _about;
    [SerializeField] private BaseStats baseStats;
    [SerializeField] private Animations _animations;
    [SerializeField] private Restrictions _restrictions;
    [SerializeField] private UnitsPf _unitsPf;
    [SerializeField] private Fraction Fraction;
    
    public About About => _about;
    public BaseStats BaseStats => baseStats;
    public Animations Animations => _animations;
    public Restrictions Restrictions => _restrictions;

    public UnitsPf UnitsPf => _unitsPf;
    
    public Fraction HeroFraction => Fraction;
}

[Serializable]
public struct About
{
    public string unitName;
    public Sprite mainImage;
    [TextArea(6, 10)] public string shortDescription;
    [TextArea(15, 10)] public string LongDescription;
}

[Serializable]
public struct BaseStats
{
    public float damage;
    public float attackRadius;
    public float maxHitPoints;
    public float armor;
    public float baseAttackDelay;
    public float moveSpeed;
    public string skill1Description;
    public float skill1Value;
    public string skill2Description;
    public float skill2Value;
}

[Serializable]
public struct Animations
{
    public AnimationReferenceAsset attack1;
    public AnimationReferenceAsset attack2;
    public AnimationReferenceAsset death;
    public AnimationReferenceAsset fly;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset move;
    public AnimationReferenceAsset skill;
    public AnimationReferenceAsset stun;
}

[Serializable]
public struct Restrictions
{
    public bool canMove;
    public bool canAttack;
    public bool canSpelled;
    public bool canAnimated;
    public bool canTakeDamage;
    public bool haveUiBar;
}

[Serializable]
public struct UnitsPf
{
    public GameObject mainPf;
    public ProjectileArrow projectilePf;
    public GameObject uiPf;
    public GameObject skill1Pf;
    public GameObject skill2Pf;
}