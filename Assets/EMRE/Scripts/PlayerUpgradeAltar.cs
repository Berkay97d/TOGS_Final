using LazyDoTween.Core;
using NaughtyAttributes;
using UnityEngine;

namespace EMRE.Scripts
{
    public class PlayerUpgradeAltar : MonoBehaviour
    {
        [SerializeField] private DoLazyToggleGroup lazyToggleGroup;
        [SerializeField] private GameObject buttonMain;
        
        [Button()]
        public void Enable()
        {
            buttonMain.SetActive(true);
            lazyToggleGroup.Enable();
        }

        [Button()]
        public void Disable()
        {
            lazyToggleGroup.Disable();
        }

        public void OnDisableComplete()
        {
            buttonMain.SetActive(false);
        }
    }
}