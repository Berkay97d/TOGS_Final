using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;


    public ItemData Data => _data;
    
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    
    
    protected Rigidbody Body
    {
        get
        {
            if (!m_Body)
            {
                m_Body = GetComponent<Rigidbody>();
            }

            return m_Body;
        }
    }

    private Rigidbody m_Body;
    
    
    public void Throw(Vector3 force)
    {
        Body.AddForce(force, ForceMode.VelocityChange);
    }
}
