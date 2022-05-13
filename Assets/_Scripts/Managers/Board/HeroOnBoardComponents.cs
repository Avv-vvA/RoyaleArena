using System.Collections.Generic;
using _Scripts.Scriptables;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers.Board
{
    public class HeroOnBoardComponents : MonoBehaviour
    {
        [HideInInspector] public Transform cardPosTrs;
        
        [SerializeField] protected List<GameObject> stars = new List<GameObject>();
         public HeroType HeroType;
        
        public HeroesPf heroesPf;
        public int mergeLevel;

        protected Transform selected;
        protected Transform heroesTrs;
        protected Camera _cam;
        protected Vector3 _startPos;
        protected bool newPlace;
        protected RectTransform _rectTransform;
        protected Transform _t;
        protected BoardManager _boardManager;
        protected Image _image;

        public SkeletonGraphic SkeletonGraphic;
        private AnimationReferenceAsset currState;
        private Animations animations;

        public enum State
        {
            Idle,
            Slow,
            Fly,
        }
        private void Awake()
        {
            _t = GetComponent<Transform>();
            _rectTransform = GetComponent<RectTransform>();
            _cam = Camera.main;
            _boardManager = BoardManager.Instance;
            _image = GetComponent<Image>();
            
        }


        
        public void Initialize(HeroesPf heroesPf, Transform selected, Transform heroes)
        {
            this.heroesPf = heroesPf;
            this.selected = selected;
            this.heroesTrs = heroes;
            HeroType = heroesPf.HeroData.HeroType;
            SkeletonGraphic = heroesPf.SkeletonGraphic;
            animations = heroesPf.HeroData.Animations;
            SetAnim(State.Idle);
        }

        public void SetAnim(State state)
        {
            if (state == State.Idle)
            {
                currState = animations.idle;
                SkeletonGraphic.AnimationState.SetAnimation(0, animations.idle, true);
            }
            else if (state == State.Slow)
            {
                SkeletonGraphic.AnimationState.TimeScale = 0.5f;
            }

            else if (state == State.Fly)
            {
                if (currState == animations.fly) return;
                currState = animations.fly;
                SkeletonGraphic.AnimationState.SetAnimation(0, animations.fly, true);
            }
            else
            {
                currState = null;
                SkeletonGraphic.AnimationState.ClearTracks();
            }
        }
        
        public void ReturnAnimSpeed()
        {
            SkeletonGraphic.AnimationState.TimeScale = 1;
        }
    }
}
