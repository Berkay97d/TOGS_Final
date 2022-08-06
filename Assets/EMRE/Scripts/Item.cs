using System;
using System.Collections;
using EMRE.Scripts;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;


    public ItemData Data => _data;
    
    public bool CanCollect { get; private set; }
    
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


    protected virtual void Start()
    {
        Invoke(nameof(MakeCollectible), _data.collectableDelay);
    }

    private void MakeCollectible()
    {
        CanCollect = true;
    }


    public void Throw(Vector3 force)
    {
        Body.AddForce(force, ForceMode.VelocityChange);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
