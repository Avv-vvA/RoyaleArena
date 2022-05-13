
// ReSharper disable once CheckNamespace

using System;
using _Scripts.Scriptables;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
  public static event Action<GameState> OnBeforeStateChange;
  public static event Action<GameState> OnAfterStateChange;

  public GameState State { get; private set; }


  private void Start() => ChangeState(GameState.Starting);



  public void ChangeState(GameState newState)
  {
    OnBeforeStateChange?.Invoke(newState);

    State = newState;

    switch (newState)
    {
      case GameState.Starting:
        HandleStarting();
        break;
      case GameState.SpawningHeroes:
        HandleSpawnHeroes();
        break;
      case GameState.Lose:
        break;
      case GameState.Win:
        break;

      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }

    OnAfterStateChange?.Invoke(newState);


  }

  private void HandleStarting()
  {
    // Do somethink 

    ChangeState(GameState.SpawningHeroes);

  }

  private void HandleSpawnHeroes()
  {
   // UnitManager.Instance.SpawnHeroes();
  }


  #region Characters Bar

  public CharacterBar CharactersBar => charactersBar;

  [Header("CharacterBar Settings")] [SerializeField]
  private CharacterBar charactersBar;

  [Serializable]
  public class CharacterBar
  {
    [SerializeField] private HpBarType hpBarType;
    [SerializeField] private bool popupDamageText;

    public bool PopupDamageText => popupDamageText;
    public HpBarType HpBarTyp => hpBarType;

    public enum HpBarType
    {
      Numbers,
      Sprite,
    }
  }

  #endregion


  #region Fraction

  public Fractions FractionsData => fractions;

  [Header("Fractions")] [SerializeField] private Fractions fractions;

  [Serializable]
  public class Fractions
  {

    [Header("Attack Ratio")] [Header("Range Cards")] [SerializeField]
    private float simpleRangeDmgRatio;

    [SerializeField] private float rareRangeDmgRatio;
    [SerializeField] private float uniqueRangeDmgRatio;


    [Header("Defence Ratio")] [SerializeField]
    private float simpleRangeDefRatio;

    [SerializeField] private float rareRangeDefRatio;
    [SerializeField] private float uniqueRangeDefRatio;

    [Header("Attack Ratio")] [Header("Melee Cards")] [SerializeField]
    private float simpleMeleeDmgRatio;

    [SerializeField] private float rareMeleeDmgRatio;
    [SerializeField] private float uniqueMeleeDmgRatio;

    [Header("Defence Ratio")] [SerializeField]
    private float simpleMeleeDefRatio;

    [SerializeField] private float rareMeleeDefRatio;
    [SerializeField] private float uniqueMeleeDefRatio;

    [Header("Attack Ratio")] [Header("Mage Cards")] [SerializeField]
    private float simpleMageDmgRatio;

    [SerializeField] private float rareMageDmgRatio;
    [SerializeField] private float uniqueMageDmgRatio;

    [Header("Defence Ratio")] [SerializeField]
    private float simpleMageDefRatio;

    [SerializeField] private float rareMageDefRatio;
    [SerializeField] private float uniqueMageDefRatio;

    [Header("Attack Ratio")] [Header("Tank Cards")] [SerializeField]
    private float simpleTankDmgRatio;

    [SerializeField] private float rareTankDmgRatio;
    [SerializeField] private float uniqueTankDmgRatio;

    [Header("Defence Ratio")] [SerializeField]
    private float simpleTankDefRatio;

    [SerializeField] private float rareTankDefRatio;
    [SerializeField] private float uniqueTankDefRatio;


    
    public float DefRatio(HeroRare heroRare, Fraction fraction)
    {
      if (heroRare == HeroRare.Basic)
      {
        if (fraction == Fraction.Melee)
        {
          return simpleMeleeDefRatio;
        }
        if (fraction == Fraction.Range)
        {
          return simpleRangeDefRatio;
        }

        if (fraction == Fraction.Mage)
        {
          return simpleMageDefRatio;
        }

        if (fraction == Fraction.Tank)
        {
          return simpleTankDefRatio;
        }
      
      }

      if (heroRare == HeroRare.Rare)
      {
        if (fraction == Fraction.Melee)
        {
          return rareMeleeDefRatio;
        }

        if (fraction == Fraction.Range)
        {
          return rareRangeDefRatio;
        }

        if (fraction == Fraction.Mage)
        {
          return rareMageDefRatio;
        }

        if (fraction == Fraction.Tank)
        {
          return rareTankDefRatio;
        }
      }

      if (heroRare == HeroRare.Unique)
      {
        if (fraction == Fraction.Melee)
        {
          return uniqueMeleeDefRatio;
        }

        if (fraction == Fraction.Range)
        {
          return uniqueRangeDefRatio;
        }

        if (fraction == Fraction.Mage)
        {
          return uniqueMageDefRatio;
        }

        if (fraction == Fraction.Tank)
        {
          return uniqueTankDefRatio;
        }
      }

      return 0;
    }

    public float DmgRatio(HeroRare heroRare, Fraction fraction)
    {
      if (heroRare == HeroRare.Basic)
      {
        if (fraction == Fraction.Melee)
        {
          return simpleMeleeDmgRatio;
        }

        if (fraction == Fraction.Range)
        {
          return simpleRangeDmgRatio;
        }

        if (fraction == Fraction.Mage)
        {
          return simpleMageDmgRatio;
        }

        if (fraction == Fraction.Tank)
        {
          return simpleTankDmgRatio;
        }
      }

      if (heroRare == HeroRare.Rare)
      {
        if (fraction == Fraction.Melee)
        {
          return rareMeleeDmgRatio;
        }

        if (fraction == Fraction.Range)
        {
          return rareRangeDmgRatio;
        }

        if (fraction == Fraction.Mage)
        {
          return rareMageDmgRatio;
        }

        if (fraction == Fraction.Tank)
        {
          return rareTankDmgRatio;
        }
      }

      if (heroRare == HeroRare.Unique)
      {
        if (fraction == Fraction.Melee)
        {
          return uniqueMeleeDmgRatio;
        }

        if (fraction == Fraction.Range)
        {
          return uniqueRangeDmgRatio;
        }

        if (fraction == Fraction.Mage)
        {
          return uniqueMageDmgRatio;
        }

        if (fraction == Fraction.Tank)
        {
          return uniqueTankDmgRatio;
        }
      }
    
      return 0;
    }
  
    }
    


    #endregion
  }


















[Serializable]
public enum GameState
{
   Starting = 0,
   SpawningHeroes = 1,
   SpawningEnemies =2,
   Win = 3,
   Lose = 4,
}

