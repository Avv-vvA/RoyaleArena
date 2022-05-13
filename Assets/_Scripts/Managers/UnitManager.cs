using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers.Board;
using _Scripts.Scriptables;
using _Scripts.Systems;
using _Scripts.Units.Hero;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] private Transform allHeroesSpawnTrs;
    [SerializeField] private Transform allEnemiesSpawnTrs;
    [SerializeField] private List<Transform> playerSpawnPositions = new List<Transform>();
    [SerializeField] private List<Transform> enemySpawnPositions = new List<Transform>();

    [SerializeField] private List<HeroType> playerSpawnCardsList = new List<HeroType>();
    [SerializeField] private List<HeroType> enemySpawnCardsList = new List<HeroType>();


    [SerializeField] private float enemySpawnTimer;
    [SerializeField] private float playerSpawnTimer;
    [SerializeField] private Image spawnFill;

    [SerializeField] private MergeType _mergeType;
    public MergeType MergeType => _mergeType;

    public List<HeroType> PlayerSpawnCardsList => playerSpawnCardsList;
    public List<HeroType> EnemySpawnCardsList => enemySpawnCardsList;

    private float enemyTimer;
    private float playerTimer;
    private BoardManager boardManager;
    private ResourceSystem resources;


    private readonly List<HeroController> spawnedHero = new List<HeroController>();
    private readonly List<HeroController> spawnedEnemy = new List<HeroController>();

    
    private void OnValidate()
    {
        if (playerSpawnCardsList.Count > 5)
        {
            playerSpawnCardsList = new List<HeroType>(new HeroType[5]);
        }

        if (enemySpawnCardsList.Count > 5)
        {
            enemySpawnCardsList = new List<HeroType>(new HeroType[5]);
        }
    }

    private void Start()
    {
        boardManager = BoardManager.Instance;
        resources = ResourceSystem.Instance;
        enemyTimer = enemySpawnTimer;
        playerTimer = playerSpawnTimer;
    }

    private void Update()
    {
        enemyTimer -= Time.deltaTime;
        playerTimer -= Time.deltaTime;
        
        
        spawnFill.fillAmount = playerTimer / playerSpawnTimer;
        if (enemyTimer < 0)
        {
            enemyTimer = enemySpawnTimer;
          
            StartCoroutine(CreateEnemyHeroes());
        }

        if (playerTimer < 0)
        {
            playerTimer = playerSpawnTimer;
            StartCoroutine(CreatePlayerHeroes());
        }
    }


    IEnumerator CreateEnemyHeroes()
    {
        if (EnemySpawnCardsList.Count <= 0) yield break;
        for (int i = 0; i < EnemySpawnCardsList.Count; i++)
        {
            {
                enemySpawnPositions[i].gameObject.SetActive(true);
                var enemyCtr = Instantiate(resources.assetsPf.EnemyPf, allEnemiesSpawnTrs);
                var hero = Instantiate(resources.GetHero(EnemySpawnCardsList[i]).UnitsPf.mainPf, enemyCtr.transform);
                enemyCtr.transform.position = enemySpawnPositions[i].position;
                spawnedEnemy.Add(enemyCtr);
              //  enemyCtr.SetMergeLevel(boardManager.willSpawnHeroes[i].mergeLevel);
            }
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < spawnedEnemy.Count; i++)
        {
            spawnedEnemy[i].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < enemySpawnPositions.Count; i++)
        {
            enemySpawnPositions[i].gameObject.SetActive(false);
        }

        spawnedEnemy.Clear();
    }


    IEnumerator CreatePlayerHeroes()
    {
        if (playerSpawnCardsList.Count <= 0) yield break;

        for (int i = 0; i < boardManager.willSpawnHeroes.Count; i++)
        {
            if (boardManager.willSpawnHeroes[i] != null)
            {
                playerSpawnPositions[boardManager.willSpawnHeroes[i].cardPosTrs.GetSiblingIndex()].gameObject
                    .SetActive(true);

                var heroCtr = Instantiate(resources.assetsPf.HeroPf, allHeroesSpawnTrs);
                var hero = Instantiate(boardManager.willSpawnHeroes[i].heroesPf.HeroData.UnitsPf.mainPf,
                    heroCtr.transform);
                spawnedHero.Add(heroCtr);
                heroCtr.transform.position =
                    playerSpawnPositions[boardManager.willSpawnHeroes[i].cardPosTrs.GetSiblingIndex()].position;
            }
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < spawnedHero.Count; i++)
        {
            spawnedHero[i].gameObject.SetActive(true);
            spawnedHero[i].SetMergeLevel(boardManager.willSpawnHeroes[i].mergeLevel);
            spawnedHero[i].t_rotate.rotation = new Quaternion(0, 0, -180, 0);
            
        }

        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < playerSpawnPositions.Count; i++)
        {
            playerSpawnPositions[i].gameObject.SetActive(false);
        }

        
        spawnedHero.Clear();
    }
}


[Serializable]
public enum MergeType
{
    basic,
    random,
}

[Serializable]
public enum Fraction
{
    Melee,
    Mage,
    Range,
    Tank,
}

[Serializable]
public enum BuffType
{
    Armor,
    AttackSpeed,
    SkillResistant
}