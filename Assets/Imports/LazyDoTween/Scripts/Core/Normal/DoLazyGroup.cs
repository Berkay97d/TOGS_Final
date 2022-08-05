using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace LazyDoTween.Core
{
    public class DoLazyGroup : MonoBehaviour
    {
        [Header("Timing")]
        [SerializeField] private float duration;

        [Header("Looping")] 
        [SerializeField, Min(-1)] private int loops = -1;
        [SerializeField] private LoopType loopType = LoopType.Restart;
        [SerializeField] private bool looping;


        public float Duration => duration;

        public int Loops => loops;
        public LoopType LoopType => loopType;
        public bool Looping => looping;


        private ILazyPlay[] m_Playables;

        private IEnumerable<ILazyPlay> Playables
        {
            get
            {
                if (m_Playables == null)
                {
                    m_Playables = GetComponents<ILazyPlay>();
                }

                return m_Playables;
            }
        }


        [Button(enabledMode: EButtonEnableMode.Playmode)]
        public void Play()
        {
            foreach (var playable in Playables)
            {
                playable.Play();
            }
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        public void Kill()
        {
            foreach (var playable in Playables)
            {
                playable.Kill();
            }
        }
    }
}