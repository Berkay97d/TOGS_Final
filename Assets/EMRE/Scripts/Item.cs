using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private Rigidbody m_Body;

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
    
    
    public void Throw()
    {
        var force = Random.insideUnitSphere * 10f;
        force.y = 10f;
        Body.AddForce(force, ForceMode.VelocityChange);
    }
}
