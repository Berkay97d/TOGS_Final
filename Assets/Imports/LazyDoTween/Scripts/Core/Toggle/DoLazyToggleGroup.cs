using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace LazyDoTween.Core
{
    public class DoLazyToggleGroup : MonoBehaviour
    {
        [Header("Timing")]
        [SerializeField] private float duration;


        public float Duration => duration;
        
        
        private ILazyToggle[] m_Toggles;

        private IEnumerable<ILazyToggle> Toggles
        {
            get
            {
                if (m_Toggles == null)
                {
                    m_Toggles = GetComponents<ILazyToggle>();
                }

                return m_Toggles;
            }
        }


        [Button(enabledMode: EButtonEnableMode.Playmode)]
        public void Enable()
        {
            foreach (var toggle in Toggles)
            {
                toggle.Enable();
            }
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        public void Disable()
        {
            foreach (var toggle in Toggles)
            {
                toggle.Disable();
            }
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        public void Kill()
        {
            foreach (var toggle in Toggles)
            {
                toggle.Kill();
            }
        }
    }
}