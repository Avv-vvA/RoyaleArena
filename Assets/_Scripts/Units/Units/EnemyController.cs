using _Scripts.Managers;
using Components;
using UnityEngine;

namespace _Scripts.Units.Units
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private UnitData unitData;
        [SerializeField] private UnitBar _unitBar;
        public UnitData UnitData => unitData;


        public Category Category;


        public UnitHandler unitHandler;

        public float currHitPoints;
        private State state;

        private float finalDefRatio;
        private float finalDmgRatio;
        public float FinalDmgRatio => finalDmgRatio;
        public float FinalDefRatio => finalDefRatio;

       // private bool canUseSpell;
      //  private bool isMoving;
      //  private float attackDelayRestart;
        private State currState;
        public Transform t;

        public void SetState(State cm)
        {
            //  canUseSpell = true;
          //  isMoving = false;
            currState = state;
            state = State.Null;
            if (cm == State.Attack)
            {
             //   attackDelayRestart = 0;
                state = State.Attack;
            }
            else if (cm == State.Move && unitData.Restrictions.canMove)
            {
               // isMoving = true;
                //   Anim.PlayMove();
                state = cm;
            }
            else if (cm == State.Skill)
            {
                // Anim.PlaySkill();
                state = cm;
                if (currState == State.Idle)
                {
                    return;
                }

                currState = State.Move;
            }
            else if (cm == State.Idle)
            {
                // Anim.PlayIdle();
                state = cm;
            }
            else if (cm == State.Stun && unitData.Restrictions.canSpelled)
            {
            //    canUseSpell = false;
                //  Anim.PlayStun();
                state = cm;
            }
            else if (cm == State.Freeze && unitData.Restrictions.canSpelled)
            {
               // canUseSpell = false;
                //  Anim.PlayFreeze();
                state = cm;
            }
            else if (cm == State.Death)
            {
               // canUseSpell = false;
                //  Anim.PlayDeath();
                state = cm;
            }
            else if (cm == State.Fly)
            {
               // canUseSpell = false;
                // Anim.PlayFly();
                state = cm;
            }
            else
            {
                state = State.Null;
            }
        }


        private void Update()
        {
            if (state == State.Move)
            {
                
            }
            else if (state == State.Attack)
            {
            }
            else if (state == State.Stun)
            {
                unitHandler.StunEffect();
            }
            else if (state == State.Freeze)
            {
                unitHandler.FreezEffect();
            }
        }

        public Vector3 GetPosition()
        {
            return t.position;
        }

        public Transform GetRotation()
        {
            return t;
        }
        
        
        private void Start()
        {
            t = GetComponent<Transform>();
            currHitPoints = unitData.BaseStats.maxHitPoints;
            finalDmgRatio = GameManager.Instance.FractionsData.DmgRatio(unitData.HeroRare, unitData.HeroFraction);
            finalDefRatio = GameManager.Instance.FractionsData.DefRatio(unitData.HeroRare, unitData.HeroFraction);
            unitHandler = new UnitHandler(this, _unitBar);

            if (Category == Category.Player)
            {
                Battlefield.Instance.playerUnits.Add(unitHandler);
            }
            else
            {
                Battlefield.Instance.enemyUnits.Add(unitHandler);
            }
        }
    }
}