using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackRayDistance;
    
    [Range(0.1f, 5)] public float attackSpeed = 1f;
    public int attackDamage = 1;
    public Transform attackPoint;
    public Vector3 attackPointOffset = new Vector3(0, 0, 0);
    
    public Vector3 attackRange = new Vector3(1,1,1);

    public LayerMask combatMask;
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.position + attackPointOffset, attackRange);
        }
    }
}
