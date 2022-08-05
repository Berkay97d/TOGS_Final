using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

namespace LazyDoTween.Core
{
    public class DoLazyToggleRotate : DoLazyToggleBase<Transform, Vector3>
    {
        [SerializeField] private bool useLocalRotate;


        private TweenerCore<Quaternion, Vector3, QuaternionOptions> m_Tween;
        
        
        public override void Enable()
        {
            Toggle(To, EnableEase, EnableEaseCurve, onEnable);
        }

        public override void Disable()
        {
            Toggle(From, DisableEase, DisableEaseCurve, onDisable);
        }

        public override void Kill()
        {
            m_Tween?.Kill();
        }


        private void Toggle(Vector3 to, Ease ease, AnimationCurve easeCurve, UnityEvent onComplete)
        {
            Kill();

            m_Tween = useLocalRotate
                ? Component.DOLocalRotate(to, Duration)
                : Component.DORotate(to, Duration);

            if (SpeedBased)
            {
                m_Tween.SetSpeedBased();
            }

            if (UseAnimationCurve)
            {
                m_Tween.SetEase(easeCurve);
            }
            else
            {
                m_Tween.SetEase(ease);
            }

            m_Tween.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}