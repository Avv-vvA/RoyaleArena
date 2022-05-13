using System;
using System.Collections;
using _Scripts.Scriptables;
using _Scripts.Units;
using _Scripts.Units.Heroes.Types;
using Interface;
using Spine.Unity;
using UnityEngine;
using _Scripts.Managers;

// ReSharper disable All

namespace Components
{
    public class Components : MonoBehaviour
    {
        [HideInInspector] public SkeletonAnimation SkeletonAnimation;
        [HideInInspector] public Transform t_move;
        [HideInInspector] public bool isMoving;
        [HideInInspector] public bool canUseSpell;

        [HideInInspector] public State State;

        // [HideInInspector] public CircleCollider2D objColl;
        [HideInInspector] public float attackDelayRestart;
        [HideInInspector] public State currState;
        [HideInInspector] public State state;
        [HideInInspector] public IDamageable target;
        [HideInInspector] public Transform t_rotate;

        public HeroesBar HeroesBar;
        public Category Category;
        public HeroData HeroData;
        public IHittable hittable;
        public Anim Anim;
        public Stats Stats;
        public Handler Handler;

        private CharactersRendererSort rendererSort;
        protected HeroesPf _heroesPf;


        private void Awake()
        {
            t_move = transform;
            _heroesPf = GetComponentInChildren<HeroesPf>();
            HeroData = _heroesPf.HeroData;
            SkeletonAnimation = _heroesPf.SkeletonAnimation;
            t_rotate = _heroesPf.transform;
            rendererSort = SkeletonAnimation.gameObject.AddComponent<CharactersRendererSort>();
            Stats = new Stats(HeroData.BaseStats, HeroData);
            Stats.Initialize();
            Anim = new Anim(this.Stats, HeroData.Animations, SkeletonAnimation);
            Handler = new Handler(this, Stats);

            if (Category == Category.Player)
            {
                Battlefield.Instance.playerUnits.Add(Handler);
            }
            else
            {
                Battlefield.Instance.enemyUnits.Add(Handler);
            }

            AddHittable();
        }
        
        public void SetState(State cm)
        {
            if (!HeroData.Restrictions.canAnimated) return;
            canUseSpell = true;
            isMoving = false;
            currState = state;
            state = State.Null;
            if (cm == State.Attack)
            {
                attackDelayRestart = 0;
                state = State.Attack;
            }
            else if (cm == State.Move && HeroData.Restrictions.canMove)
            {
                isMoving = true;
                Anim.PlayMove();
                state = cm;
            }
            else if (cm == State.Skill)
            {
                Anim.PlaySkill();
                state = cm;
                if (currState == State.Idle)
                {
                    return;
                }

                currState = State.Move;
            }
            else if (cm == State.Idle)
            {
                Anim.PlayIdle();
                state = cm;
            }
            else if (cm == State.Stun && HeroData.Restrictions.canSpelled)
            {
                canUseSpell = false;
                Anim.PlayStun();
                state = cm;
            }
            else if (cm == State.Freeze && HeroData.Restrictions.canSpelled)
            {
                canUseSpell = false;
                Anim.PlayFreeze();
                state = cm;
            }
            else if (cm == State.Death)
            {
                canUseSpell = false;
                Anim.PlayDeath();
                state = cm;
            }
            else if (cm == State.Fly)
            {
                canUseSpell = false;
                Anim.PlayFly();
                state = cm;
            }
            else
            {
                state = State.Null;
            }
        }


        public void ReturnToCurrState()
        {
            if (state == State.Freeze)
            {
                currState = State.Move;
                return;
            }

            if (currState == State.Idle)
            {
                SetState(State.Idle);
                return;
            }

            SetState(State.Move);
            currState = State.Null;
        }

        public Vector3 GetPosition()
        {
            return t_move.position;
        }


        private void AddHittable()
        {
            switch (HeroData.HeroType)
            {
                case HeroType.Billy:
                    hittable = gameObject.AddComponent<Billy>();
                    break;
                case HeroType.Dilly:
                    hittable = gameObject.AddComponent<Dilly>();
                    break;
                case HeroType.Willy:
                    hittable = gameObject.AddComponent<Willy>();
                    break;
                case HeroType.Kitsu:
                    hittable = new Kitsu(this);
                    break;
                case HeroType.Bonny:
                    hittable = new Bonny(this);
                    break;
                case HeroType.Gerra:
                    hittable = new Gerra(this);
                    break;
                case HeroType.Jacke:
                    hittable = gameObject.AddComponent<Jacke>();
                    break;
                case HeroType.Joker:
                    hittable = new Joker(this);
                    break;
                case HeroType.Rade:
                    hittable = new Rade(this);
                    break;
                case HeroType.Ramona:
                    hittable = new Ramona(this);
                    break;
                case HeroType.Skadi:
                    hittable = gameObject.AddComponent<Skadi>();
                    break;
            }
        }


        public Transform GetRotation()
        {
            return t_rotate;
        }


        public void HideCharacter(float delay)
        {
            if (rendererSort != null)
            {
                StartCoroutine(Death(delay));
                _heroesPf.Shadow.SetActive(false);
            }
        }

        IEnumerator Death(float delay)
        {
            if (delay != 0)
                yield return new WaitForSeconds(delay);

            for (int i = 0; i < 10; i++)
            {
                SkeletonAnimation.skeleton.A -= 0.1f;
                yield return new WaitForSeconds(0.09f);
            }

            if (HeroData.HeroType == HeroType.Skadi)
            {
                Destroy(gameObject.GetComponent<Skadi>().iceEffect);
            }

            Destroy(t_move.gameObject);
        }
    }


    [Serializable]
    public enum State
    {
        Idle,
        Move,
        Attack,
        Skill,
        Death,
        Fly,
        Freeze,
        Stun,
        Null,
    }

    [Serializable]
    public enum Category
    {
        Player,
        Enemy,
    }
}