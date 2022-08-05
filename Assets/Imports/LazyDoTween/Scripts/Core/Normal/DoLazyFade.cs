using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace LazyDoTween.Core
{
    public class DoLazyFade : DoLazyBase<Graphic, float>
    {
        private TweenerCore<Color, Color, ColorOptions> m_Tween;

        
        public override void Play()
        {
            Kill();
            
            m_Tween = Component.DOFade(To, Duration);
            
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