using UnityEngine;

namespace EMRE.Scripts
{
    public class FarmLandSign : MonoBehaviour
    {
        [SerializeField] private GameObject lockedText;
        [SerializeField] private SpriteRenderer spriteRenderer;


        public void Unlock(Sprite icon)
        {
            lockedText.SetActive(false);
            spriteRenderer.gameObject.SetActive(true);
            SetIcon(icon);
        }

        public void SetIcon(Sprite icon)
        {
            spriteRenderer.sprite = icon;
        }
    }
}