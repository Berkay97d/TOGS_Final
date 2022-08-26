using UnityEngine;

public class WorkerRandomer : MonoBehaviour
{
    [SerializeField] private Material[] bodyMaterials;
    [SerializeField] private new Renderer renderer;


    public void Randomize()
    {
        RandomBodyColor();
    }

    private void RandomBodyColor()
    {
        var randomIndex = Random.Range(0, bodyMaterials.Length);
        var randomBodyMaterial = bodyMaterials[randomIndex];
        renderer.sharedMaterial = randomBodyMaterial;
    }
}
