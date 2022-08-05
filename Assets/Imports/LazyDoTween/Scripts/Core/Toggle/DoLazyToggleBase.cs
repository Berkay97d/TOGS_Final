using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace LazyDoTween.Core
{
    public abstract class DoLazyToggleBase<TComponent, TValue> : MonoBehaviour, ILazyToggle
    where TComponent : Component
    {
        [Header("References")]
        [SerializeField] private TComponent component;

        [Header("Values")] 
        [SerializeField] private TValue from;
        [SerializeField] private TValue to;

        [Header("Timing")]
        [SerializeField, Min(0f)] private float duration;
        [SerializeField] private bool speedBased;

        [Header("Easing")] 
        [SerializeField] private Ease enableEase = Ease.Linear;
        [SerializeField] private AnimationCurve enableEaseCurve;
        [SerializeField] private Ease disableEase = Ease.Linear;
        [SerializeField] private AnimationCurve disableEaseCurve;
        [SerializeField] private bool useAnimationCurve;


        [Header("Events")]
        public UnityEvent onEnable;
        public UnityEvent onDisable;


        protected TValue From => from;
        protected TValue To => to;

        protected float Duration => ToggleGroup ? ToggleGroup.Duration : duration;
        protected bool SpeedBased => speedBased;

        protected Ease EnableEase => enableEase;
        protected AnimationCurve EnableEaseCurve => enableEaseCurve;
        protected Ease DisableEase => disableEase;
        protected AnimationCurve DisableEaseCurve => disableEaseCurve;
        protected bool UseAnimationCurve => useAnimationCurve;
        
        
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

        private DoLazyToggleGroup ToggleGroup
        {
            get
            {
                if (!m_ToggleGroup)
                {
                    m_ToggleGroup = GetComponent<DoLazyToggleGroup>();
                }

                return m_ToggleGroup;
            }
        }


        private TComponent m_Component;
        private DoLazyToggleGroup m_ToggleGroup;
        

        public abstract void Enable();
        public abstract void Disable();
        public abstract void Kill();
    }
}