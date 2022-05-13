using _Scripts.Scriptables;
using Spine.Unity;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class HeroesPf : MonoBehaviour
{
  [SerializeField] private HeroData _heroData;
  [SerializeField] private SkeletonGraphic _skeletonGraphic;
  [SerializeField] private SkeletonAnimation _skeletonAnimation;
  [SerializeField] private GameObject shadow;
  [SerializeField] private Transform projectileSpawnPos;

  public SkeletonGraphic SkeletonGraphic => _skeletonGraphic;
  public SkeletonAnimation SkeletonAnimation => _skeletonAnimation;
  public HeroData HeroData => _heroData;
  public GameObject Shadow => shadow;

  public Transform ProjectileSpawnPos => projectileSpawnPos;
}
