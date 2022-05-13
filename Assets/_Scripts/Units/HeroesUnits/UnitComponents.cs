using _Scripts.Units.Units;
using Components;
using Interface;
using Spine.Unity;
using UnityEngine;

namespace _Scripts.Units.HeroesUnits
{
    public class UnitComponents : MonoBehaviour
    {
        [SerializeField] private UnitData _unitData;
        public UnitData UnitData => _unitData;

        [HideInInspector] public SkeletonAnimation SkeletonAnimation;
        [HideInInspector] public Transform t_move;
        [HideInInspector] public bool isMoving;
        [HideInInspector] public bool canUseSpell;
        [HideInInspector] public float attackDelayRestart;
        [HideInInspector] public State currState;
        [HideInInspector] public State state;

        [HideInInspector] public IDamageable target;

        [HideInInspector] public Transform t_rotate;


        public Category Category;
        public IHittable hittable;
        public Anim Anim;
        public Stats Stats;
        public UnitHandler Handler;


        private void Awake()
        {
            t_move = transform;
            SkeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            t_rotate = t_move;


            Stats = new Stats();
            Handler = new UnitHandler(this);
            hittable = new Wolf(this);
            Anim = new Anim(Stats, _unitData.Animations, SkeletonAnimation);
        }

        protected void SetState(State cm)
        {
            if (!_unitData.Restrictions.canAnimated) return;
            canUseSpell = true;
            isMoving = false;
            currState = state;
            state = State.Null;
            if (cm == State.Attack)
            {
                attackDelayRestart = 0;
                state = State.Attack;
            }
            else if (cm == State.Move && _unitData.Restrictions.canMove)
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
            else if (cm == State.Stun && _unitData.Restrictions.canSpelled)
            {
                canUseSpell = false;
                Anim.PlayStun();
                state = cm;
            }
            else if (cm == State.Freeze && _unitData.Restrictions.canSpelled)
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

        public Transform GetRotation()
        {
            return t_rotate;
        }
    }
}