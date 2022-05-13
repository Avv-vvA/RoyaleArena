using System.Collections.Generic;
using _Scripts.Scriptables;
using UnityEngine;


namespace Components
{
    public class Stats
    {
        private bool isHeroController;

        public Stats(BaseStats baseStats, HeroData heroData)
        {
            this.baseStats = baseStats;
            this.heroData = heroData;
            isHeroController = true;
        }


        public Stats()
        {
        }


        private readonly BaseStats baseStats;
        private readonly HeroData heroData;

        public int buffLevel;
        public int mergeLevel;

        private float finalAttackDelay;
        private float baseAttackDelay;

        private float finalDamage;
        private float finalArmor;

        private float finalMaxHitPoints;
        public float currHitPoints;

        private float finalDefRatio;
        private float finalDmgRatio;

        private float finalAttackAnimationSpeed;

        private float finalSkill1Values;
        private float finalSkill2Values;


        private float buffArmor;
        private float buffAttackSpeed;
        private float skillResistance;


        #region Public Stats

        public float skillAttackSpeedUpdate;
        public float FinalSkill1Values => finalSkill1Values;

        public float FinalSkill2Values => finalSkill2Values;

        public float FinalDamage => finalDamage;

        public float FinalMaxHitPoints => finalMaxHitPoints;
        public float FinalDmgRatio => finalDmgRatio;

        public float FinalAttackDelay =>
            1 / (1 / baseAttackDelay * (1 + (0.01f * (skillAttackSpeedUpdate + buffAttackSpeed))));


        public float FinalAttackAnimationSpeed => finalAttackDelay;

        public float FinalArmor => finalArmor + buffArmor;
        public float FinalDefRatio => finalDefRatio;

        #endregion

        #region Upgrading System

        private void CardLevelUpgrade()
        {
            int currLvl = heroData.cardLevel;
            finalDamage = baseStats.damage + heroData.CardLevelsUpdate.AttackUpgrade[currLvl];
            finalArmor = baseStats.armor + heroData.CardLevelsUpdate.ArmorUpgrade[currLvl];
            finalMaxHitPoints = baseStats.maxHitPoints + heroData.CardLevelsUpdate.HitPointUpgrade[currLvl];
            finalSkill1Values = baseStats.skill1Value + heroData.CardLevelsUpdate.Skill1Upgrade[currLvl];
            finalSkill2Values = baseStats.skill2Value + heroData.CardLevelsUpdate.Skill2Upgrade[currLvl];
            baseAttackDelay = baseStats.baseAttackDelay + heroData.CardLevelsUpdate.AttackDelayUpgrade[currLvl];
            finalAttackDelay = baseAttackDelay;
            finalAttackAnimationSpeed = 1 / finalAttackDelay + 0.1f;
        } // Стартовое повышение всех характеристик карты  После запукается Merge => Buff

        public void CardMergeUpdate()
        {
            finalSkill1Values += heroData.CardMergesUpdate.Skill1Upgrade[mergeLevel];
            finalSkill2Values += heroData.CardMergesUpdate.Skill2Upgrade[mergeLevel];
            finalArmor += heroData.CardMergesUpdate.ArmorUpgrade[mergeLevel];
            finalDamage += heroData.CardMergesUpdate.AttackUpgrade[mergeLevel];
            finalMaxHitPoints += heroData.CardMergesUpdate.HitPointUpgrade[mergeLevel];
            finalAttackDelay += heroData.CardMergesUpdate.AttackDelayUpgrade[mergeLevel];
        } // запускается автоматически после улучшения уровни карты 

        private void CardBuffLevelUpdate()
        {
            finalSkill1Values += heroData.CardBuffsUpdate.Skill1Upgrade[buffLevel];
            finalSkill2Values += heroData.CardBuffsUpdate.Skill2Upgrade[buffLevel];
            finalArmor += heroData.CardBuffsUpdate.ArmorUpgrade[buffLevel];
            finalDamage += heroData.CardBuffsUpdate.AttackUpgrade[buffLevel];
            finalMaxHitPoints += heroData.CardBuffsUpdate.HitPointUpgrade[buffLevel];
            finalAttackDelay += heroData.CardBuffsUpdate.AttackDelayUpgrade[buffLevel];
        } // запускается автоматичеески после улучшения мерджа карты 

        #endregion


        #region Buff System

        private readonly List<float> armorsBuffList = new List<float>();
        private readonly List<float> attackSpeedsBufList = new List<float>();
        private readonly List<float> skillResistanceBuffList = new List<float>();
        
        public void SetBuff(BuffType buffType, float value)
        {
            switch (buffType)
            {
                case BuffType.Armor:

                    armorsBuffList.Add(value);

                    if (value > buffArmor)
                    {
                        buffArmor = value;
                    }

                    break;
                case BuffType.AttackSpeed:

                    attackSpeedsBufList.Add(value);

                    if (value > buffAttackSpeed)
                    {
                        buffAttackSpeed = value;
                    }

                    break;
                case BuffType.SkillResistant:
                    
                    skillResistanceBuffList.Add(value);

                    if (value > skillResistance)
                    {
                        skillResistance = value;
                    }

                    break;
            }
        }

        public void RemoveBuff(BuffType buffType, float value) // убрать бафы
        {
            switch (buffType)
            {
                case BuffType.Armor:

                    armorsBuffList.Remove(value);

                    if (armorsBuffList.Count > 0)
                    {
                        armorsBuffList.Sort();
                        buffArmor = armorsBuffList[armorsBuffList.Count - 1];
                    }
                    else
                    {
                        buffArmor = 0;
                    }


                    break;
                case BuffType.AttackSpeed:
                    attackSpeedsBufList.Remove(value);

                    if (attackSpeedsBufList.Count > 0)
                    {
                        attackSpeedsBufList.Sort();
                        buffAttackSpeed = attackSpeedsBufList[attackSpeedsBufList.Count - 1];
                    }
                    else
                    {
                        buffAttackSpeed = 0;
                    }

                    break;
                case BuffType.SkillResistant:

                    skillResistanceBuffList.Remove(value);

                    if (skillResistanceBuffList.Count > 0)
                    {
                        skillResistanceBuffList.Sort();
                        skillResistance = skillResistanceBuffList[skillResistanceBuffList.Count - 1];
                    }
                    else
                    {
                        skillResistance = 0;
                    }

                    break;
            }
        }

        #endregion


        private void DmgDefRation() // Присвоение финальным коэфицентам урона и брони от рарности и типа карты
        {
            finalDmgRatio = GameManager.Instance.FractionsData.DmgRatio(heroData.HeroRare, heroData.HeroFraction);
            finalDefRatio = GameManager.Instance.FractionsData.DefRatio(heroData.HeroRare, heroData.HeroFraction);
        }

        public void Initialize()
        {
            if (!isHeroController) return;
            CardLevelUpgrade();
            CardMergeUpdate();
            CardBuffLevelUpdate();
            DmgDefRation();
            currHitPoints = finalMaxHitPoints;
        }


        public void WolfStats(Stats cm)
        {
            finalDamage = cm.finalDamage / 100 * cm.finalSkill2Values;
            finalAttackDelay = cm.finalAttackDelay;
            baseAttackDelay = cm.baseAttackDelay;

            finalDamage = cm.finalDamage / 100 * cm.finalSkill2Values;
            finalArmor = cm.finalArmor / 100 * cm.finalSkill2Values;

            finalMaxHitPoints = cm.finalMaxHitPoints / 100 * cm.finalSkill2Values;
            finalDefRatio = cm.finalDefRatio;
            finalDmgRatio = cm.finalDmgRatio;

            finalAttackAnimationSpeed = cm.finalAttackAnimationSpeed / 100 * cm.finalSkill2Values;

            currHitPoints = finalMaxHitPoints;
        }
    }
}