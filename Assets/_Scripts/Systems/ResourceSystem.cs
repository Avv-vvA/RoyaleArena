using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Scriptables;
using _Scripts.Units.Hero;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Systems
{
    public class ResourceSystem : Singleton<ResourceSystem>
    {
        public List<HeroType> PlayerSpawnCardsList => PlayersData.playerSpawnCardsList;
        public List<HeroData> Heroes { get; private set; }
        private Dictionary<HeroType, HeroData> _ExampleHeroesDict;
        
        [field: SerializeField] public PlayersData PlayersData { get;  private set; }
       

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }
        
        
        private void AssembleResources()
        {
            Heroes = Resources.LoadAll<HeroData>("Units/Heroes").ToList();
            _ExampleHeroesDict = Heroes.ToDictionary(r => r.HeroType, r => r);
        }

        public HeroData GetHero(HeroType t) => _ExampleHeroesDict[t];
        public HeroData GetRandomHero() => Heroes[Random.Range(0, Heroes.Count)];


        #region AssetsPf

        [SerializeField] private AssetsPf _assetsPf;
        public AssetsPf assetsPf => _assetsPf;

        [Serializable]
        public class AssetsPf
        {
            [SerializeField] private HeroController heroPf;
            [SerializeField] private HeroController enemyPf;
            [SerializeField] private CharacterViewTemplate characterViewTemplateUi;
            public HeroController HeroPf => heroPf;
            public HeroController EnemyPf => enemyPf;
            public CharacterViewTemplate CharacterViewTemplateUi => characterViewTemplateUi;
        }

        #endregion
    }
}