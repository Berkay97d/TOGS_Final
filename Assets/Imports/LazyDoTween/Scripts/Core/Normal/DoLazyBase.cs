using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace LazyDoTween.Core
{
    public abstract class DoLazyBase<TComponent, TValue> : MonoBehaviour, ILazyPlay
    where TComponent : Component
    {
        [Header("References")]
        [SerializeField] private TComponent component;

        [Header("Values")] 
        [SerializeField] private TValue from;
        [SerializeField] private TValue to;
        [SerializeField] private bool useFrom;
        
        [Header("Timing")]
        [SerializeField, Min(0f)] private float duration;
        [SerializeField] private bool speedBased;

        [Header("Easing")] 
        [SerializeField] private Ease ease = Ease.Linear;
        [SerializeField] private AnimationCurve easeCurve;
        [SerializeField] private bool useAnimationCurve;

        [Header("Looping")]
        [SerializeField, Min(-1)] private int loops = -1;
        [SerializeField] private LoopType loopType = LoopType.Restart;
        [SerializeField] private bool looping;


        [Header("Events")]
        public UnityEvent onComplete;


        protected TValue From => from;
        protected TValue To => to;
        protected bool UseFrom => useFrom;

        protected float Duration => Group ? Group.Duration : duration;

        protected bool SpeedBased => speedBased;

        protected Ease Ease => ease;
        protected AnimationCurve EaseCurve => easeCurve;
        protected bool UseAnimationCurve => useAnimationCurve;

        protected int Loops => Group ? Group.Loops : loops;
        protected LoopType LoopType => Group ? Group.LoopType : loopType;
        protected bool Looping => Group ? Group.Looping : looping;
        

        protected TComponent Component
        {
            get
            {
                if (component)
                {
                    return component;
                }

                if (!m_Component)
                {
                    m_Component = GetComponent<TComponent>();
                }

                return m_Component;
            }
        }

        private DoLazyGroup Group
        {
            get
            {
                if (!m_Group)
                {
                    m_Group = GetComponent<DoLazyGroup>();
                }

                return m_Group;
            }
        }


        private TComponent m_Component;
        private DoLazyGroup m_Group;


        public abstract void Play();
        public abstract void Kill();
    }
}