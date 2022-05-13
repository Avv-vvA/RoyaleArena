using System.Collections.Generic;
using _Scripts.Scriptables;
using _Scripts.Systems;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers.Board
{
    public class BoardManager : Singleton<BoardManager>
    {
        [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
        [SerializeField] private Transform heroesOnBoard;
        [SerializeField] private Transform selected;
        [SerializeField] private HeroOnBoard heroOnBoardPf;
        [SerializeField] private Button spawnHero;
        [SerializeField] private Material blackAndWhite;
        [SerializeField] private Material main;
       
        
        public List<HeroOnBoard> cardInDesc = new List<HeroOnBoard>();
        public List<HeroOnBoard> willSpawnHeroes = new List<HeroOnBoard>();
        
        
        
        private readonly List<Transform> freeSpawnPos = new List<Transform>();

        private UnitManager unitManager;
        private ResourceSystem resourceSystem;
        
        private void Start()
        {
            unitManager = UnitManager.Instance;
            resourceSystem = ResourceSystem.Instance;
            spawnHero.onClick.AddListener(BuyHero);


            foreach (var t in spawnPositions)
            {
                freeSpawnPos.Add(t);
            }
        }


        private void BuyHero()
        {
            if (freeSpawnPos.Count > 0)
            {
                var rndPos = RandomPosition();
                var obj = CreateRandomHero();
                obj.SetPlace(freeSpawnPos[rndPos]);
                freeSpawnPos.Remove(freeSpawnPos[rndPos]);
            }
            else
            {
                Debug.Log("У вас нет свободного места");
            }
        }

        private int RandomPosition()
        {
            for (int i = 0; i < freeSpawnPos.Count; i++)
            {
                if (freeSpawnPos[i].GetSiblingIndex() < 5)
                {
                    return i;
                }
            }

            return Random.Range(0, freeSpawnPos.Count);
        }

        public HeroOnBoard CreateRandomHero()
        {
            var obj = Instantiate(heroOnBoardPf, heroesOnBoard, false);
            var obj2 = Instantiate(resourceSystem.GetHero(GetRandomHero()).UnitsPf.uiPf, obj.transform,
                false);
            obj2.transform.SetSiblingIndex(0);
            obj.Initialize(obj2.GetComponent<HeroesPf>(), selected, heroesOnBoard);
            cardInDesc.Add(obj);
            return obj;
        }
        
        
        private HeroType GetRandomHero()
        {
            var rnd = Random.Range(0, 5);
            return unitManager.PlayerSpawnCardsList[rnd];
        }

        #region OnBoards

        public void CanUpdateCheck(HeroOnBoard cardCm)
        {
            foreach (var t in cardInDesc)
            {
                if (cardCm.HeroType == HeroType.Joker && t.mergeLevel == cardCm.mergeLevel||
                    t.HeroType == HeroType.Joker && t.mergeLevel == cardCm.mergeLevel)
                {
                    
                }
                else if (cardCm.HeroType != t.HeroType || t.mergeLevel != cardCm.mergeLevel)
                {
                    t.SkeletonGraphic.material = blackAndWhite;
                    t.SetAnim(HeroOnBoardComponents.State.Slow);
                }
            }

            cardCm.SkeletonGraphic.material = main;
        }


        public void ColoredCardInDesc()
        {
            foreach (var t in cardInDesc)
            {
                t.SkeletonGraphic.material = main;
                t.ReturnAnimSpeed();
            }
        }


        public void AddFreePlace(Transform freePlace)
        {
            freeSpawnPos.Add(freePlace);
        }

        public void RemoveFreePlace(Transform place)
        {
            freeSpawnPos.Remove(place);
        }

        public void RemoveCardInDesc(HeroOnBoard heroesBoard)
        {
            cardInDesc.Remove(heroesBoard);
        }

        #endregion
    }
}