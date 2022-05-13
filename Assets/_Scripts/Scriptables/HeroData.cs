using System;
using UnityEngine;

namespace _Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Hero", order = 51)]
    public class HeroData : UnitBase
    {
        [Header("Main")] [SerializeField] private HeroType heroType;
        [SerializeField] private HeroRare heroRare;
        [SerializeField] private AttackType attackType;

        public int cardLevel;


        public AttackType AttackType => attackType;

        public HeroType HeroType => heroType;

        public HeroRare HeroRare => heroRare;


        #region Cards Level Update Data

        public CardLevelUpdate CardLevelsUpdate => cardLevelUpdate;

        [Header("Card Upgrading")] [SerializeField]
        private CardLevelUpdate cardLevelUpdate;

        [Serializable]
        public class CardLevelUpdate
        {
            [SerializeField]
            private float[] attackUpgrade = new float[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            [SerializeField]
            private float[] hitPointUpgrade = new float[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            [SerializeField]
            private float[] armorUpgrade = new float[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            [SerializeField]
            private float[] attackDelayUpgrade = new float[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            [SerializeField]
            private float[] skill1Upgrade = new float[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            [SerializeField]
            private float[] skill2Upgrade = new float[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            public float[] AttackUpgrade => attackUpgrade;
            public float[] HitPointUpgrade => hitPointUpgrade;
            public float[] ArmorUpgrade => armorUpgrade;
            public float[] AttackDelayUpgrade => attackDelayUpgrade;

            public float[] Skill1Upgrade => skill1Upgrade;

            public float[] Skill2Upgrade => skill2Upgrade;
        }

        #endregion

        #region Cards Merge Update Data

        public CardMergeUpdate CardMergesUpdate => cardMergeUpdate;

        [SerializeField] private CardMergeUpdate cardMergeUpdate;

        [Serializable]
        public class CardMergeUpdate
        {
            [SerializeField] private float[] attackUpgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] hitPointUpgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] armorUpgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] attackDelayUpgrade = new float[] {0, 0, 0, 0, 0, 0};

            [SerializeField] private float[] skill1Upgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] skill2Upgrade = new float[] {0, 0, 0, 0, 0, 0};

            public float[] AttackUpgrade => attackUpgrade;
            public float[] HitPointUpgrade => hitPointUpgrade;
            public float[] ArmorUpgrade => armorUpgrade;
            public float[] AttackDelayUpgrade => attackDelayUpgrade;

            public float[] Skill1Upgrade => skill1Upgrade;

            public float[] Skill2Upgrade => skill2Upgrade;
        }

        #endregion

        #region Cards BuffUpdate

        public CardBuffUpdate CardBuffsUpdate => cardBuffUpdate;

        [SerializeField] private CardBuffUpdate cardBuffUpdate;

        [Serializable]
        public class CardBuffUpdate
        {
            [SerializeField] private float[] attackUpgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] hitPointUpgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] armorUpgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] attackDelayUpgrade = new float[] {0, 0, 0, 0, 0, 0};

            [SerializeField] private float[] skill1Upgrade = new float[] {0, 0, 0, 0, 0, 0};
            [SerializeField] private float[] skill2Upgrade = new float[] {0, 0, 0, 0, 0, 0};

            public float[] AttackUpgrade => attackUpgrade;
            public float[] HitPointUpgrade => hitPointUpgrade;
            public float[] ArmorUpgrade => armorUpgrade;
            public float[] AttackDelayUpgrade => attackDelayUpgrade;

            public float[] Skill1Upgrade => skill1Upgrade;

            public float[] Skill2Upgrade => skill2Upgrade;
        }

        #endregion
    }


    [Serializable]
    public enum HeroType
    {
        Bonny,
        Skadi,
        Gerra,
        Jacke,
        Joker,
        Ramona,
        Rade,
        Willy,
        Billy,
        Dilly,
        Gorana,
        Pango,
        Kitsu,
        Witch,
        Devin,
        Nekro,
        Elfy,
    }

    [Serializable]
    public enum AttackType
    {
        Melee,
        Range,
        Null,
    }

    [Serializable]
    public enum HeroRare
    {
        Basic,
        Rare,
        Unique,
        Epic,
    }
}