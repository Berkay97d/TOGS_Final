using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace LazyDoTween.Core
{
    public class DoLazyScale : DoLazyBase<Transform, Vector3>
    {
        private TweenerCore<Vector3, Vector3, VectorOptions> m_Tween;
        
        
        public override void Play()
        {
            Kill();
            m_Tween = Component.DOScale(To, Duration);

            if (UseFrom)
            {
                m_Tween.From(From);
            }

            if (SpeedBased)
            {
                m_Tween.SetSpeedBased();
            }

            if (UseAnimationCurve)
            {
                m_Tween.SetEase(EaseCurve);
            }
            else
            {
                m_Tween.SetEase(Ease);
            }

            if (Looping)
            {
                m_Tween.SetLoops(Loops, LoopType);
            }

            m_Tween.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
        
        public override void Kill()
        {
            m_Tween?.Kill();
        }
    }
}